using System;
using Core.Object.Service;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    /// <summary>
    /// A local service that detects when the mouse pointer enters or exits the module, re-raising Unity's pointer events as subscribable events.
    /// </summary>
    public class MousePointerEventHandler : LocalClientBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private MousePointerEventDispatcher _mousePointerEventDispatcher;

        public override void Install()
        {
            Console.LogProgress();

            _mousePointerEventDispatcher = Resolver.GetLocalService<MousePointerEventDispatcher>();

            base.Install();
        }

        private void OnDestroy()
        {
            Console.LogProgress();

            _mousePointerEventDispatcher = null;
        }

        /// <summary>
        /// Called when the mouse pointer enters the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer enter event.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse pointer is entered.");

            _mousePointerEventDispatcher.EnterMousePointer(eventData);
        }

        /// <summary>
        /// Called when the mouse pointer exits the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer exit event.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse pointer is exited.");

            _mousePointerEventDispatcher.ExitMousePointer(eventData);
        }
    }
}
