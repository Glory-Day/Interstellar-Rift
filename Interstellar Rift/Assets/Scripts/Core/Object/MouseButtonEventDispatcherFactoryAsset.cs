using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Object
{
    [CreateAssetMenu(fileName = "Mouse Button Event Dispatcher Factory Asset",
                     menuName = "Assets/Services/Local/Mouse Button Event Dispatcher")]
    public class MouseButtonEventDispatcherFactoryAsset : LocalServiceFactoryAsset
    {
        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(MouseButtonEventDispatcher).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new MouseButtonEventDispatcher(resolver);
        }
    }
}
