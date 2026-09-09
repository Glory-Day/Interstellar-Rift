using System;
using Core.Object.Service;
using UnityEngine.EventSystems;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    public class MouseButtonEventDispatcher : LocalService
    {
        public MouseButtonEventDispatcher(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            OnMouseButtonPressed = null;
            OnMouseButtonReleased = null;
            OnMouseButtonDragged = null;
        }

        public void PressMouseButton(PointerEventData eventData)
        {
            OnMouseButtonPressed?.Invoke(eventData);
        }

        public void ReleaseMouseButton(PointerEventData eventData)
        {
            OnMouseButtonReleased?.Invoke(eventData);
        }

        public void DragMouseButton(PointerEventData eventData)
        {
            OnMouseButtonDragged?.Invoke(eventData);
        }

        public event Action<PointerEventData> OnMouseButtonPressed;

        public event Action<PointerEventData> OnMouseButtonReleased;

        public event Action<PointerEventData> OnMouseButtonDragged;
    }
}
