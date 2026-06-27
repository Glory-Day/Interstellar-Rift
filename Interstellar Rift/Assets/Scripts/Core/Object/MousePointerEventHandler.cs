using System;
using UnityEngine;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    public class MousePointerEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private void OnDestroy()
        {
            Console.LogProgress();

            OnMousePointerEntered = null;
            OnMousePointerExited = null;
        }

        /// <summary>
        /// Called when the mouse pointer enters the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer enter event.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse pointer is entered.");

            OnMousePointerEntered?.Invoke(eventData);
        }

        /// <summary>
        /// Called when the mouse pointer exits the module.
        /// </summary>
        /// <param name="eventData">The event data associated with the pointer exit event.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse pointer is exited.");

            OnMousePointerExited?.Invoke(eventData);
        }

        /// <summary>
        /// Occurs when the mouse pointer enters the module.
        /// </summary>
        public event Action<PointerEventData> OnMousePointerEntered;

        /// <summary>
        /// Occurs when the mouse pointer exits the module.
        /// </summary>
        public event Action<PointerEventData> OnMousePointerExited;
    }
}
