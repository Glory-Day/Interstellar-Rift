using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Model Resolver Builder Factory Asset",
                     menuName = "Assets/Services/Local/Module Model Resolver Builder")]
    public class ModuleModelResolverBuilderFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleModelResolverBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleModelResolverBuilder(resolver);
        }
    }
}
