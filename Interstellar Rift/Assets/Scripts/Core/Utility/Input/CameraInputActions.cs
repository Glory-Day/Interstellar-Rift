using System;
using UnityEngine.InputSystem;

namespace Core.Utility.Input
{
    /// <summary>
    /// An <see cref="IGameInputActions"/> that wraps the generated <see cref="MapInputActions"/> "Camera" action map,
    /// exposing camera zoom input as subscribable events.
    /// </summary>
    public class CameraInputActions : IGameInputActions
    {
        private readonly MapInputActions _inputActions;

        /// <param name="inputActions">The generated <see cref="MapInputActions"/> to read camera input from.</param>
        public CameraInputActions(MapInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        /// <inheritdoc/>
        public void Enable()
        {
            _inputActions.Camera.Enable();
            _inputActions.Camera.Zoom.started += OnZoomStarted_Internal;
            _inputActions.Camera.Zoom.performed += OnZoomPerformed_Internal;
            _inputActions.Camera.Zoom.canceled += OnZoomCanceled_Internal;

            IsEnabled = true;
        }

        /// <inheritdoc/>
        public void Disable()
        {
            _inputActions.Camera.Zoom.started -= OnZoomStarted_Internal;
            _inputActions.Camera.Zoom.performed -= OnZoomPerformed_Internal;
            _inputActions.Camera.Zoom.canceled -= OnZoomCanceled_Internal;
            _inputActions.Camera.Disable();

            IsEnabled = false;
        }

        /// <summary>
        /// Forwards the zoom action's <c>started</c> phase to <see cref="OnZoomStarted"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the zoom input value.</param>
        private void OnZoomStarted_Internal(InputAction.CallbackContext context)
        {
            OnZoomStarted?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the zoom action's <c>performed</c> phase to <see cref="OnZoomPerformed"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the zoom input value.</param>
        private void OnZoomPerformed_Internal(InputAction.CallbackContext context)
        {
            OnZoomPerformed?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the zoom action's <c>canceled</c> phase to <see cref="OnZoomCanceled"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the zoom input value.</param>
        private void OnZoomCanceled_Internal(InputAction.CallbackContext context)
        {
            OnZoomCanceled?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Occurs when the zoom input starts, passing the current zoom value.
        /// </summary>
        public event Action<float> OnZoomStarted;

        /// <summary>
        /// Occurs every frame the zoom input is being performed, passing the current zoom value.
        /// </summary>
        public event Action<float> OnZoomPerformed;

        /// <summary>
        /// Occurs when the zoom input is canceled, passing the zoom value at cancellation.
        /// </summary>
        public event Action<float> OnZoomCanceled;

        /// <inheritdoc/>
        public bool IsEnabled { get; private set; }
    }
}
