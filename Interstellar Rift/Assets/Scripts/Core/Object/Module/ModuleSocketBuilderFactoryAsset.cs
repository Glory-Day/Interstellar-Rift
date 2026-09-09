using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Socket Builder Factory Asset",
                     menuName = "Assets/Services/Local/Module Socket Builder")]
    public class ModuleSocketBuilderFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleSocketBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleSocketBuilder(resolver);
        }
    }
}
