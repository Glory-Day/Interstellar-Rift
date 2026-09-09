using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Drag Visual Effect Factory Asset",
                     menuName = "Assets/Services/Local/Module Drag Visual Effect")]
    public class ModuleDragVisualEffectFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleDragVisualEffect).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleDragVisualEffect(resolver);
        }
    }
}
