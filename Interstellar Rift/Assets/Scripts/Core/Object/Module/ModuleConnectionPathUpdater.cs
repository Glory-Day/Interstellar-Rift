using Core.Object.Service;
using Core.Utility.Mathematics;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleConnectionPathUpdater : LocalService
    {
        #region LOCAL SERVICE API

        private ModuleConnectionPathCacher _moduleConnectionPathCacher;
        private ModuleSocket _moduleSocket;

        #endregion

        public ModuleConnectionPathUpdater(ModuleConnectionPathCacher moduleConnectionPathCacher, ModuleSocket moduleSocket)
        {
            Console.LogProgress();

            _moduleConnectionPathCacher = moduleConnectionPathCacher;
            _moduleSocket = moduleSocket;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _moduleConnectionPathCacher = null;
            _moduleSocket = null;
        }

        public void Update(SearchedSlotResult? result)
        {
            if (result is null)
            {
                _moduleConnectionPathCacher.Clear();

                return;
            }

            var target = result.Value;
            var slot = target.Slot.transform;
            Vector2 start = _moduleSocket.Joint.transform.position;
            Vector2 end = slot.position;

            Vector2 outward = slot.up;
            var tangent = Vector2.Perpendicular(outward);

            var offset = start - end;
            var axial = Mathf.Max(Vector2.Dot(offset, outward), 0f);
            var lateral = Vector2.Dot(offset, tangent);

            var a = end + outward * (axial * (2f / 3f)) + tangent * (lateral * 0.5f);
            var b = end + outward * (axial * (1f / 3f));

            _moduleConnectionPathCacher.Update(new BezierCurve(start, a, b, end));
        }
    }
}
