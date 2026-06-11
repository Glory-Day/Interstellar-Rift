using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Utility.Input
{
    public class DefaultModuleInputActions : IGameInputActions
    {
        private readonly ModuleInputActions _inputActions;

        public DefaultModuleInputActions(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void Enable()
        {
            _inputActions.Default.Enable();
            _inputActions.Default.Click.started += OnClickStarted_Internal;
            _inputActions.Default.Click.performed += OnClickPerformed_Internal;
            _inputActions.Default.Click.canceled += OnClickCanceled_Internal;
            _inputActions.Default.Dragging.started += OnDraggingStarted_Internal;
            _inputActions.Default.Dragging.performed += OnDraggingPerformed_Internal;
            _inputActions.Default.Dragging.canceled += OnDraggingCanceled_Internal;

            IsEnabled = true;
        }

        public void Disable()
        {
            _inputActions.Default.Click.started -= OnClickStarted_Internal;
            _inputActions.Default.Click.performed -= OnClickPerformed_Internal;
            _inputActions.Default.Click.canceled -= OnClickCanceled_Internal;
            _inputActions.Default.Dragging.started -= OnDraggingStarted_Internal;
            _inputActions.Default.Dragging.performed -= OnDraggingPerformed_Internal;
            _inputActions.Default.Dragging.canceled -= OnDraggingCanceled_Internal;
            _inputActions.Default.Disable();

            IsEnabled = false;
        }

        private void OnClickStarted_Internal(InputAction.CallbackContext context)
        {
            OnClickStarted?.Invoke(context.ReadValue<float>());
        }

        private void OnClickPerformed_Internal(InputAction.CallbackContext context)
        {
            OnClickPerformed?.Invoke(context.ReadValue<float>());
        }

        private void OnClickCanceled_Internal(InputAction.CallbackContext context)
        {
            OnClickCanceled?.Invoke(context.ReadValue<float>());
        }

        private void OnDraggingStarted_Internal(InputAction.CallbackContext context)
        {
            OnDraggingStarted?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnDraggingPerformed_Internal(InputAction.CallbackContext context)
        {
            OnDraggingPerformed?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnDraggingCanceled_Internal(InputAction.CallbackContext context)
        {
            OnDraggingCanceled?.Invoke(context.ReadValue<Vector2>());
        }

        public event Action<float> OnClickStarted;

        public event Action<float> OnClickPerformed;

        public event Action<float> OnClickCanceled;

        public event Action<Vector2> OnDraggingStarted;

        public event Action<Vector2> OnDraggingPerformed;

        public event Action<Vector2> OnDraggingCanceled;

        public bool IsEnabled { get; private set; }
    }
}
