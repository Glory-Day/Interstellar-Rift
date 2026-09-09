using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTransformResolver : LocalService
    {
        public ModuleTransformResolver(Transform transform, ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            Main = transform;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            Main = null;
        }

        public Transform Main { get; private set; }
    }
}
