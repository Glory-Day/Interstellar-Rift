using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Connector Factory Asset",
                     menuName = "Assets/Services/Local/Module Connector")]
    public class ModuleConnectorFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var updateEventDispatcher = resolver.GetGlobalService<UpdateEventDispatcher>();

            var moduleConnectionPathUpdater = resolver.GetLocalService<ModuleConnectionPathUpdater>();
            var moduleConnectionMovement = resolver.GetLocalService<ModuleConnectionMovement>();
            var moduleConnectionVisualEffector = resolver.GetLocalService<ModuleConnectionVisualEffector>();

            var moduleSearcher = resolver.GetLocalService<ModuleSearcher>();
            var moduleSocket = resolver.GetLocalService<ModuleSocket>();

            Console.LogSuccess($"{nameof(ModuleConnector).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleConnector(updateEventDispatcher,
                                       moduleConnectionPathUpdater, moduleConnectionMovement, moduleConnectionVisualEffector,
                                       moduleSearcher, moduleSocket);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
