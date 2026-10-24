using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Texture Drawer Builder Factory Asset",
                     menuName = "Assets/Services/Local/Module Texture Drawer Builder")]
    public class ModuleTextureDrawerBuilderFactoryAsset : LocalServiceBuilderFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var colorApplicator = resolver.GetLocalService<ColorApplicator>();

            Console.LogSuccess($"{nameof(ModuleTextureDrawerBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleTextureDrawerBuilder(colorApplicator);
        }

        public override InstallationMode Mode => InstallationMode.Automatic;
    }
}
