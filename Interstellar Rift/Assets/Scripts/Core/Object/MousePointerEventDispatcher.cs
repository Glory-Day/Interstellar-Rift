using System;
using Core.Object.Service;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    public class MousePointerEventDispatcher : LocalService, IMousePointerEventDispatcher
    {
        public MousePointerEventDispatcher(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            OnMousePointerEntered = null;
            OnMousePointerExited = null;
        }

        public void EnterMousePointer(PointerEventData eventData)
        {
            OnMousePointerEntered?.Invoke(eventData);
        }

        public void ExitMousePointer(PointerEventData eventData)
        {
            OnMousePointerExited?.Invoke(eventData);
        }

        public event Action<PointerEventData> OnMousePointerEntered;

        public event Action<PointerEventData> OnMousePointerExited;
    }
}
