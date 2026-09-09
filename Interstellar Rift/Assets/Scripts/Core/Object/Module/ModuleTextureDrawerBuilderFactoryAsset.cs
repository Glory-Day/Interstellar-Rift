using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Texture Drawer Builder Factory Asset",
                     menuName = "Assets/Services/Local/Module Texture Drawer Builder")]
    public class ModuleTextureDrawerBuilderFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleTextureDrawerBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleTextureDrawerBuilder(resolver);
        }
    }
}
