using UnityEngine;

namespace Core.Utility.Mathematics
{
    public static class BezierCurveExtensions
    {
        public static BezierCurve ToLocalSpace(this BezierCurve path, Transform transform)
        {
            return new BezierCurve(transform.InverseTransformPoint(path.Point0),
                                   transform.InverseTransformPoint(path.Point1),
                                   transform.InverseTransformPoint(path.Point2),
                                   transform.InverseTransformPoint(path.Point3));
        }
    }
}
