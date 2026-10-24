using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Drag Movement Factory Asset",
                     menuName = "Assets/Services/Local/Module Drag Movement")]
    public class ModuleDragMovementFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var camera = resolver.GetGlobalService<Camera>();

            var transform = resolver.GetLocalService<ModuleTransformResolver>().Main;

            Console.LogSuccess($"{nameof(ModuleDragMovement).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleDragMovement(camera, transform);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
