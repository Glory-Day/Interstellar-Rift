using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Socket Builder Factory Asset",
                     menuName = "Assets/Services/Local/Module Socket Builder")]
    public class ModuleSocketBuilderFactoryAsset : LocalServiceBuilderFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var transform = resolver.GetLocalService<ModuleTransformResolver>().Main;

            Console.LogSuccess($"{nameof(ModuleSocketBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleSocketBuilder(transform);
        }

        public override InstallationMode Mode => InstallationMode.Automatic;
    }
}
