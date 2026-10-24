using System;
using Core.Object.Service;
using Core.Utility;
using Core.Utility.Mathematics;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleConnectionPathCacher : LocalService, ICacher<BezierCurve>
    {
        private BezierCurve _path;

        public override void Dispose()
        {
            Console.LogProgress();
        }

        public void Update(BezierCurve? path)
        {
            if (path is null)
            {
                IsCached = false;

                return;
            }

            _path = path.Value;

            IsCached = true;
        }

        public BezierCurve Fetch()
        {
            return IsCached ? _path : throw new InvalidOperationException("Connection path is not cached yet.");;
        }

        public void Clear()
        {
            IsCached = false;
        }

        public bool IsCached { get; private set; }
    }
}
