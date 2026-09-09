using System;
using UnityEngine.EventSystems;

namespace Core.Object
{
    public interface IMouseButtonEventDispatcher
    {
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
