using System;
using Core.Object.Service;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    /// <summary>
    /// A local service that detects when the mouse pointer enters or exits the module, re-raising Unity's pointer events as subscribable events.
    /// </summary>
    public class MouseButtonEventHandler : LocalClientBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        private MouseButtonEventDispatcher _mouseButtonEventDispatcher;

        public override void Install()
        {
            Console.LogProgress();

            _mouseButtonEventDispatcher = Resolver.GetLocalService<MouseButtonEventDispatcher>();

            base.Install();
        }

        private void OnDestroy()
        {
            Console.LogProgress();

            _mouseButtonEventDispatcher = null;
        }

        /// <summary>
        /// Called when the mouse button is released over the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer up event.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is released.");

            _mouseButtonEventDispatcher.ReleaseMouseButton(eventData);
        }

        /// <summary>
        /// Called when the mouse button is pressed over the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer down event.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is pressed.");

            _mouseButtonEventDispatcher.PressMouseButton(eventData);
        }

        /// <summary>
        /// Called every frame while the module is being dragged.
        /// </summary>
        /// <param name="eventData">The event data associated with the drag event.</param>
        public void OnDrag(PointerEventData eventData)
        {
            _mouseButtonEventDispatcher.DragMouseButton(eventData);
        }
    }
}
