using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Connection Path Cacher Factory Asset",
                     menuName = "Assets/Services/Local/Module Connection Path Cacher")]
    public class ModuleConnectionPathCacherFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleConnectionPathCacher).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleConnectionPathCacher();
        }

        public override InstallationMode Mode => InstallationMode.Automatic;
    }
}
