using UnityEngine;

namespace Core.Utility.Mathematics
{
    public readonly struct BezierCurve
    {
        public readonly Vector2 Point0;
        public readonly Vector2 Point1;
        public readonly Vector2 Point2;
        public readonly Vector2 Point3;

        public BezierCurve(Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3)
        {
            Point0 = point0;
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

        /// <summary>
        /// Evaluates the curve at the given parameter.
        /// </summary>
        /// <param name="time">Bézier interpolation parameter (t), clamped to the [0, 1] range.</param>
        /// <returns>The interpolated point on the curve.</returns>
        public Vector2 Evaluate(float time)
        {
            var t = Mathf.Clamp01(time);
            var u = 1f - t;

            var point = u * u * u * Point0;
            point += 3f * u * u * t * Point1;
            point += 3f * u * t * t * Point2;
            point += t * t * t * Point3;

            return point;
        }

        /// <summary>
        /// Evaluates the first derivative of the curve at the given parameter.
        /// </summary>
        /// <param name="time">Bézier interpolation parameter (t), clamped to the [0, 1] range.</param>
        /// <returns>The (non-normalized) derivative vector at the given parameter.</returns>
        public Vector2 Derivative(float time)
        {
            var t = Mathf.Clamp01(time);
            var u = 1f - t;

            var derivative = 3f * u * u * (Point1 - Point0);
            derivative += 6f * u * t * (Point2 - Point1);
            derivative += 3f * t * t * (Point3 - Point2);

            return derivative;
        }

        /// <summary>
        /// Returns the unit tangent of the curve at the given parameter, pointing in the direction of increasing (t).
        /// Falls back to <see cref="Direction"/> when the derivative has no length.
        /// </summary>
        /// <param name="time">Bézier interpolation parameter (t), clamped to the [0, 1] range.</param>
        /// <returns>The unit tangent at the given parameter.</returns>
        public Vector2 Tangent(float time)
        {
            var derivative = Derivative(time);

            return derivative.sqrMagnitude > 0f ? derivative.normalized : Direction;
        }

        /// <summary>
        /// Unit vector of the chord pointing from <see cref="Point0"/> to <see cref="Point3"/>.
        /// Falls back to <see cref="Vector2.right"/> when the two points coincide.
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                var chord = Point3 - Point0;

                return chord.sqrMagnitude > 0f ? chord.normalized : Vector2.right;
            }
        }

        /// <summary>
        /// Length of the chord between <see cref="Point0"/> and <see cref="Point3"/>.
        /// </summary>
        public float Distance => Vector2.Distance(Point0, Point3);
    }
}
