using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object
{
    [CreateAssetMenu(fileName = "Mouse Pointer Event Dispatcher Factory Asset",
                     menuName = "Assets/Services/Local/Mouse Pointer Event Dispatcher")]
    public class MousePointerEventDispatcherFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(MousePointerEventDispatcher).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new MousePointerEventDispatcher(resolver);
        }
    }
}
