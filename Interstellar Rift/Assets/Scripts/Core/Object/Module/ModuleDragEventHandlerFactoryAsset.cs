using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Drag Event Handler Factory Asset",
                     menuName = "Assets/Services/Local/Module Drag Event Handler")]
    public class ModuleDragEventHandlerFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleDragEventHandler).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleDragEventHandler(resolver);
        }
    }
}
