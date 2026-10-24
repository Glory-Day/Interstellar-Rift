using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Connection Path Updater Factory Asset",
                     menuName = "Assets/Services/Local/Module Connection Path Updater")]
    public class ModuleConnectionPathUpdaterFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var moduleConnectionPathCacher = resolver.GetLocalService<ModuleConnectionPathCacher>();
            var moduleSocket = resolver.GetLocalService<ModuleSocket>();

            Console.LogSuccess($"{nameof(ModuleConnectionPathUpdater).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleConnectionPathUpdater(moduleConnectionPathCacher, moduleSocket);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
