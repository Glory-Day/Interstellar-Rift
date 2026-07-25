using System;
using Core.Object.Service;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    /// <summary>
    /// A local service that detects when the mouse pointer enters or exits the module, re-raising Unity's pointer events as subscribable events.
    /// </summary>
    public class MouseButtonEventHandler : LocalServiceBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        private void OnDestroy()
        {
            Console.LogProgress();

            OnMouseButtonPressed = null;
            OnMouseButtonReleased = null;
            OnMouseButtonDragged = null;
        }

        /// <summary>
        /// Called when the mouse button is released over the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer up event.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is released.");

            OnMouseButtonReleased?.Invoke(eventData);
        }

        /// <summary>
        /// Called when the mouse button is pressed over the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer down event.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is pressed.");

            OnMouseButtonPressed?.Invoke(eventData);
        }

        /// <summary>
        /// Called every frame while the module is being dragged.
        /// </summary>
        /// <param name="eventData">The event data associated with the drag event.</param>
        public void OnDrag(PointerEventData eventData)
        {
            OnMouseButtonDragged?.Invoke(eventData);
        }

        /// <summary>
        /// Occurs when the mouse button is pressed over the module.
        /// </summary>
        public event Action<PointerEventData> OnMouseButtonPressed;

        /// <summary>
        /// Occurs when the mouse button is released over the module.
        /// </summary>
        public event Action<PointerEventData> OnMouseButtonReleased;

        /// <summary>
        /// Occurs every frame while the module is being dragged.
        /// </summary>
        public event Action<PointerEventData> OnMouseButtonDragged;
    }
}
