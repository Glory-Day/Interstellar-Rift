using System;
using UnityEngine;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    public class MouseButtonEventHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        private void OnDestroy()
        {
            Console.LogProgress();

            OnMouseButtonPressed = null;
            OnMouseButtonReleased = null;
            OnMouseButtonDragged = null;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is released.");

            OnMouseButtonReleased?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is pressed.");

            OnMouseButtonPressed?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Console.LogEventMessage("Mouse button is dragged.");

            OnMouseButtonDragged?.Invoke(eventData);
        }

        public event Action<PointerEventData> OnMouseButtonPressed;

        public event Action<PointerEventData> OnMouseButtonReleased;

        public event Action<PointerEventData> OnMouseButtonDragged;
    }
}
