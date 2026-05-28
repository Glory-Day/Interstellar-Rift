using System;
using UnityEngine.InputSystem;

namespace Core.Utility.Input
{
    public class CameraInputActions : IGameInputActions
    {
        private readonly MapInputActions _inputActions;

        public CameraInputActions(MapInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void Enable()
        {
            _inputActions.Camera.Enable();
            _inputActions.Camera.Zoom.started += OnZoomStarted_Internal;
            _inputActions.Camera.Zoom.performed += OnZoomPerformed_Internal;
            _inputActions.Camera.Zoom.canceled += OnZoomCanceled_Internal;

            IsEnabled = true;
        }

        public void Disable()
        {
            _inputActions.Camera.Zoom.started -= OnZoomStarted_Internal;
            _inputActions.Camera.Zoom.performed -= OnZoomPerformed_Internal;
            _inputActions.Camera.Zoom.canceled -= OnZoomCanceled_Internal;
            _inputActions.Camera.Disable();

            IsEnabled = false;
        }

        private void OnZoomStarted_Internal(InputAction.CallbackContext context)
        {
            OnZoomStarted?.Invoke(context.ReadValue<float>());
        }

        private void OnZoomPerformed_Internal(InputAction.CallbackContext context)
        {
            OnZoomPerformed?.Invoke(context.ReadValue<float>());
        }

        private void OnZoomCanceled_Internal(InputAction.CallbackContext context)
        {
            OnZoomCanceled?.Invoke(context.ReadValue<float>());
        }

        public event Action<float> OnZoomStarted;

        public event Action<float> OnZoomPerformed;

        public event Action<float> OnZoomCanceled;

        public bool IsEnabled { get; private set; }
    }
}
