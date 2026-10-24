using Core.Object.Service;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleConnectionPathVisualDebugger : LocalServiceInstalledVisualDebugger
    {
        private const int Segments = 24; // Number of line segments used to draw the curve.

        #region LOCAL SERVICE API

        private ModuleConnectionPathCacher _moduleConnectionPathCacher;

        #endregion

        public override void Install()
        {
            Console.LogProgress();

            _moduleConnectionPathCacher = Resolver.GetLocalService<ModuleConnectionPathCacher>();

            base.Install();
        }

        public void OnDestroy()
        {
            Console.LogProgress();

            _moduleConnectionPathCacher = null;
        }

#if UNITY_EDITOR

        protected override void DrawGizmos()
        {
            if (_moduleConnectionPathCacher == null || _moduleConnectionPathCacher.IsCached is false)
            {
                return;
            }

            var path = _moduleConnectionPathCacher.Fetch();

            Gizmos.color = Color.gray;
            Gizmos.DrawLine(path.Point0, path.Point1);
            Gizmos.DrawLine(path.Point1, path.Point2);
            Gizmos.DrawLine(path.Point2, path.Point3);

            Gizmos.DrawWireSphere(path.Point1, 0.05f);
            Gizmos.DrawWireSphere(path.Point2, 0.05f);

            Gizmos.color = Color.yellow;

            var previous = path.Point0;
            for (var i = 1; i <= Segments; i++)
            {
                var current = path.Evaluate((float)i / Segments);
                Gizmos.DrawLine(previous, current);
                previous = current;
            }

            Gizmos.DrawSphere(path.Point0, 0.1f);
            Gizmos.DrawSphere(path.Point3, 0.1f);
        }

#endif

        public override InstallationMode Mode => InstallationMode.Automatic;
    }
}
