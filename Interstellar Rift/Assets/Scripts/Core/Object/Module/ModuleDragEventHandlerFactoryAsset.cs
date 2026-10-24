using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Drag Event Handler Factory Asset",
                     menuName = "Assets/Services/Local/Module Drag Event Handler")]
    public class ModuleDragEventHandlerFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var moduleConnector = resolver.GetLocalService<ModuleConnector>();

            var moduleDragMovement = resolver.GetLocalService<ModuleDragMovement>();

            var mouseButtonEventDispatcher = resolver.GetLocalService<MouseButtonEventDispatcher>();

            Console.LogSuccess($"{nameof(ModuleDragEventHandler).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleDragEventHandler(moduleConnector,
                                              moduleDragMovement,
                                              mouseButtonEventDispatcher);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
