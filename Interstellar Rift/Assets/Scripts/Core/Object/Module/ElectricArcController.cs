using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ElectricArcController : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [Title("Positions", HorizontalLine = false)]
        [SerializeField] private Transform a;
        [SerializeField] private Transform b;
        [SerializeField] private Transform c;
        [SerializeField] private Transform d;

        [Title("Configuration")]
        [Range(0f, 1f)]
        [SerializeField] private float amount = 0.3f;

        [Title("Jitter", HorizontalLine = false)]
        [Tooltip("Noise sampling speed.")]
        [Range(0f, 5f)]
        [SerializeField] private float speed = 2f;
        [Range(0f, 0.2f)]
        [SerializeField] private float range = 0.05f;

        #endregion

        private const float CurveRadius = 0.3f;
        private const float TimeB = 0.25f;
        private const float TimeC = 0.75f;
        private const float RatioB = 1f / 3f;
        private const float RatioC = 2f / 3f;


        private Vector2 _outward = Vector2.up; // The outward direction of the target socket (d side), already reflecting the module's current rotation.
        private float _radius = 1f; // The detection radius used to search for this socket. Used to normalize the sag amount.

        private float _seed;

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(a.position, 0.05f);
            Gizmos.DrawWireSphere(b.position, 0.05f);
            Gizmos.DrawWireSphere(c.position, 0.05f);
            Gizmos.DrawWireSphere(d.position, 0.05f);

            Gizmos.DrawLine(a.position, b.position);
            Gizmos.DrawLine(b.position, c.position);
            Gizmos.DrawLine(c.position, d.position);
        }

#endif

        //TODO: You need to fix it! Change to initialize by object pool event.
        private void Awake()
        {
            Console.LogProgress();

            _seed = Random.Range(0f, 1000f);
        }

        public void UpdateTargetPoint(Vector3 spawner, SearchedSlotResult target)
        {
            a.position = spawner;
            d.position = target.Position;

            _outward = target.Outward;
            _outward = _outward.sqrMagnitude > 0f ? _outward.normalized : Vector2.up;

            _radius = target.Radius;
            _radius = Mathf.Max(_radius, 0.0001f);
        }

        public void UpdateMiddlePoints()
        {
            GetMiddlePoints(out var p1, out var p2);

            b.position = GetJitteredPoint(p1, p2, RatioB, _seed);
            c.position = GetJitteredPoint(p1, p2, RatioC, _seed + 37.13f);
        }

        private void GetMiddlePoints(out Vector2 p1, out Vector2 p2)
        {
            Vector2 p0 = a.position;
            Vector2 p3 = d.position;

            var distance = Vector2.Distance(p0, p3);

            // Tangent direction perpendicular to the slot's outward axis.
            var tangent = new Vector2(-_outward.y, _outward.x);

            // How far p0 (the moving side) deviates from the socket's central axis, normalized to [-1, 1] by the detection radius.
            // This keeps the sag amount continuous, so it smoothly passes through zero instead of snapping between opposite directions near the axis.
            var offset = p0 - p3;
            var tangential = Vector2.Dot(offset, tangent);
            var t = Mathf.Clamp(tangential / _radius, -1f, 1f);

            var direction = tangent * t;

            p1 = Vector2.Lerp(p0, p3, TimeB) + direction * (distance * amount);
            p2 = Vector2.Lerp(p0, p3, TimeC) + direction * (distance * amount * CurveRadius);
        }

        private Vector2 GetJitteredPoint(Vector2 p1, Vector2 p2, float ratio, float random)
        {
            Vector2 p0 = a.position;
            Vector2 p3 = d.position;

            var distance = Vector2.Distance(p0, p3);
            var perpendicular = GetPerpendicular(p0, p3);

            var time = Time.time * speed;
            var t = Mathf.PerlinNoise(random, time) * 2f - 1f;
            var p = Mathf.PerlinNoise(random + 91.7f, time) * 2f - 1f;

            var jitter = Mathf.Clamp01(ratio + t * range);
            var point = GetCubicBezierPoint(p0, p1, p2, p3, jitter);
            point += perpendicular * (p * amount * distance);

            return point;
        }

        private static Vector2 GetPerpendicular(Vector2 p0, Vector2 p3)
        {
            var distance = Vector2.Distance(p0, p3);
            var direction = distance > 0f ? (p3 - p0) / distance : Vector2.right;

            return new Vector2(-direction.y, direction.x);
        }

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
