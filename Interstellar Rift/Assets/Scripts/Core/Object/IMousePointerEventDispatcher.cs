using System;
using UnityEngine.EventSystems;

namespace Core.Object
{
    public interface IMousePointerEventDispatcher
    {
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
