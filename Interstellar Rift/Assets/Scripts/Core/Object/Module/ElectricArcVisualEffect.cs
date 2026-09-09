using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ElectricArcVisualEffect : ActivatableLocalService
    {
        private const float CurveRadius = 0.3f; // Scale factor applied to control point P2's tangential offset relative to P1's, so the two middle control points sag by different amounts and the curve reads as asymmetric/organic.
        private const float TimeB = 0.25f;      // Base Bézier parameter (t) used to place control point P1 along the P0 - P3 line.
        private const float TimeC = 0.75f;      // Base Bézier parameter (t) used to place control point P2 along the P0 - P3 line.
        private const float RatioB = 1f / 3f;   // Base Bézier evaluation ratio used when sampling the jittered position of point B.
        private const float RatioC = 2f / 3f;   // Base Bézier evaluation ratio used when sampling the jittered position of point C.

        private readonly Transform[] _transforms;

        private readonly float _amount;

        private readonly float _speed;
        private readonly float _radius;         // The detection radius used to search for this slot. Used to normalize the sag amount.
        private readonly float _range;

        private Vector2 _outward = Vector2.up;  // The outward direction of the target slot (d side), already reflecting the module's current rotation.

        private readonly float _seed;

        public ElectricArcVisualEffect(Transform[] transforms,
                                       float amount,
                                       float speed, float range,
                                       float seed,
                                       GameObject gameObject,
                                       ServiceResolver resolver) : base(gameObject, resolver)
        {
            Console.LogProgress();

            _transforms = transforms;

            _amount = amount;

            _speed = speed;
            _radius = Resolver.GetLocalService<ModuleSearcher>().Radius;
            _range = range;

            _seed = seed;
        }

        /// <summary>
        /// Sets the arc's start and end world positions from the spawner and a slot search result, and
        /// caches the target slot's outward direction and detection radius for use when computing sag.
        /// </summary>
        /// <param name="spawner">World position of the arc's start point (P0), typically the dragged module's slot.</param>
        /// <param name="target">Search result describing the target slot's position, outward direction, and detection radius.</param>
        public void UpdateTargetPoint(Vector3 spawner, SearchedSlotResult target)
        {
            _transforms[0].position = spawner;
            _transforms[3].position = target.Position;

            _outward = target.Outward;
            _outward = _outward.sqrMagnitude > 0f ? _outward.normalized : Vector2.up;
        }

        /// <summary>
        /// Recomputes and applies the two middle Bézier control points (P1, P2) for the current frame,
        /// combining the tangential sag offset with per-point Perlin noise jitter.
        /// </summary>
        public void UpdateMiddlePoints()
        {
            GetMiddlePoints(out var p1, out var p2);

            _transforms[1].position = GetJitteredPoint(p1, p2, RatioB, _seed);
            _transforms[2].position = GetJitteredPoint(p1, p2, RatioC, _seed + 37.13f);
        }

        /// <summary>
        /// Computes the base (pre-jitter) positions of the two middle control points along the P0 - P3 line,
        /// offset perpendicular to the slot's outward axis by a continuous, signed sag amount.
        /// </summary>
        /// <param name="p1">Outputs the base position of control point P1.</param>
        /// <param name="p2">Outputs the base position of control point P2.</param>
        private void GetMiddlePoints(out Vector2 p1, out Vector2 p2)
        {
            Vector2 p0 = _transforms[0].position;
            Vector2 p3 = _transforms[3].position;

            var distance = Vector2.Distance(p0, p3);

            // Tangent direction perpendicular to the slot's outward axis.
            var tangent = new Vector2(-_outward.y, _outward.x);

            // How far p0 (the moving side) deviates from the slot's central axis, normalized to [-1, 1] by the detection radius.
            // This keeps the sag amount continuous, so it smoothly passes through zero instead of snapping between opposite directions near the axis.
            var offset = p0 - p3;
            var tangential = Vector2.Dot(offset, tangent);
            var t = Mathf.Clamp(tangential / _radius, -1f, 1f);

            // Combine the sag direction and magnitude, then offset each base point along it;
            // P2 sags less than P1 (scaled by CurveRadius) so the curve reads as asymmetric.
            var direction = tangent * t;

            p1 = Vector2.Lerp(p0, p3, TimeB) + direction * (distance * _amount);
            p2 = Vector2.Lerp(p0, p3, TimeC) + direction * (distance * _amount * CurveRadius);
        }

        /// <summary>
        /// Evaluates a point on the cubic Bézier curve near the given base ratio, then displaces it along
        /// the curve's perpendicular using time-varying Perlin noise to produce the arc's flicker/jitter.
        /// </summary>
        /// <param name="p1">Base middle control point P1, as computed by <see cref="GetMiddlePoints"/>.</param>
        /// <param name="p2">Base middle control point P2, as computed by <see cref="GetMiddlePoints"/>.</param>
        /// <param name="ratio">Base Bézier parameter (t) around which the noise-driven jitter is clamped.</param>
        /// <param name="offset">Per-point Perlin noise seed offset, used so P1 and P2 jitter independently.</param>
        /// <returns>The jittered world-space point to assign to a middle control point's Transform.</returns>
        private Vector2 GetJitteredPoint(Vector2 p1, Vector2 p2, float ratio, float offset)
        {
            Vector2 p0 = _transforms[0].position;
            Vector2 p3 = _transforms[3].position;

            var distance = Vector2.Distance(p0, p3);
            var perpendicular = GetPerpendicular(p0, p3);

            // Sample two independent, time-varying Perlin noise values (the +91.7f offset keeps them decorrelated),
            // remapped from [0, 1] to [-1, 1].
            // (t) drives along-curve jitter, (p) drives perpendicular jitter.
            var time = Time.time * _speed;
            var t = Mathf.PerlinNoise(offset, time) * 2f - 1f;
            var p = Mathf.PerlinNoise(offset + 91.7f, time) * 2f - 1f;

            // Wobble the base ratio by t to get a jittered curve parameter, sample the curve at that parameter,
            // then push the result perpendicular to the curve by p, scaled with distance.
            var jitter = Mathf.Clamp01(ratio + t * _range);
            var point = GetCubicBezierPoint(p0, p1, p2, p3, jitter);
            point += perpendicular * (p * _amount * distance);

            return point;
        }

        /// <summary>
        /// Returns the unit vector perpendicular to the direction from <paramref name="p0"/> to <paramref name="p3"/>.
        /// Falls back to the perpendicular of <see cref="Vector2.right"/> when the two points coincide.
        /// </summary>
        /// <param name="p0">Start point of the reference direction.</param>
        /// <param name="p3">End point of the reference direction.</param>
        /// <returns>A unit vector perpendicular to the P0 - P3 direction.</returns>
        private static Vector2 GetPerpendicular(Vector2 p0, Vector2 p3)
        {
            var distance = Vector2.Distance(p0, p3);
            var direction = distance > 0f ? (p3 - p0) / distance : Vector2.right;

            return new Vector2(-direction.y, direction.x);
        }

        /// <summary>
        /// Evaluates the standard cubic Bézier formula for the given control points at parameter <paramref name="time"/>.
        /// </summary>
        /// <param name="p0">Start point of the curve (t = 0).</param>
        /// <param name="p1">First control point.</param>
        /// <param name="p2">Second control point.</param>
        /// <param name="p3">End point of the curve (t = 1).</param>
        /// <param name="time">Bézier interpolation parameter (t), typically in the [0, 1] range.</param>
        /// <returns>The interpolated point on the cubic Bézier curve.</returns>
        private static Vector2 GetCubicBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float time)
        {
            var u = 1f - time;
            var point = u * u * u * p0;
            point += 3f * u * u * time * p1;
            point += 3f * u * time * time * p2;
            point += time * time * time * p3;

            return point;
        }
    }
}
