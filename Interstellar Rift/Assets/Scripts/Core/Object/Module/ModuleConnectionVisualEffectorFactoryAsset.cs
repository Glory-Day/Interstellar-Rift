using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Connection Visual Effector Factory Asset",
                     menuName = "Assets/Services/Local/Module Connection Visual Effector")]
    public class ModuleConnectionVisualEffectorFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var updateEventDispatcher = resolver.GetGlobalService<UpdateEventDispatcher>();

            var electricArcVisualEffect = resolver.GetLocalService<ElectricArcVisualEffect>();

            Console.LogSuccess($"{nameof(ModuleConnectionVisualEffector).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleConnectionVisualEffector(updateEventDispatcher, electricArcVisualEffect);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
