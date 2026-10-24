using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Transform Resolver Builder Factory Asset",
                     menuName = "Assets/Services/Local/Module Transform Resolver Builder")]
    public class ModuleTransformResolverBuilderFactoryAsset : LocalServiceBuilderFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleTransformResolverBuilder).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ModuleTransformResolverBuilder();
        }

        public override InstallationMode Mode => InstallationMode.Manual;
    }
}
