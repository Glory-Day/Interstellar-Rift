using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Utility.Input
{
    public class BoosterModuleInputActions : IGameInputActions
    {
        private readonly ModuleInputActions _inputActions;

        public BoosterModuleInputActions(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void Enable()
        {
            _inputActions.Booster.Enable();
            _inputActions.Booster.Move.started += OnMoveStarted_Internal;
            _inputActions.Booster.Move.performed += OnMovePerformed_Internal;
            _inputActions.Booster.Move.canceled += OnMoveCanceled_Internal;
            _inputActions.Booster.Rotate.started += OnRotateStarted_Internal;
            _inputActions.Booster.Rotate.performed += OnRotatePerformed_Internal;
            _inputActions.Booster.Rotate.canceled += OnRotateCanceled_Internal;

            IsEnabled = true;
        }

        public void Disable()
        {
            _inputActions.Booster.Move.started -= OnMoveStarted_Internal;
            _inputActions.Booster.Move.performed -= OnMovePerformed_Internal;
            _inputActions.Booster.Move.canceled -= OnMoveCanceled_Internal;
            _inputActions.Booster.Rotate.started -= OnRotateStarted_Internal;
            _inputActions.Booster.Rotate.performed -= OnRotatePerformed_Internal;
            _inputActions.Booster.Rotate.canceled -= OnRotateCanceled_Internal;
            _inputActions.Booster.Disable();

            IsEnabled = false;
        }

        private void OnMoveStarted_Internal(InputAction.CallbackContext context)
        {
            OnMoveStarted?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMovePerformed_Internal(InputAction.CallbackContext context)
        {
            OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMoveCanceled_Internal(InputAction.CallbackContext context)
        {
            OnMoveCanceled?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnRotateStarted_Internal(InputAction.CallbackContext context)
        {
            OnRotateStarted?.Invoke(context.ReadValue<float>());
        }

        private void OnRotatePerformed_Internal(InputAction.CallbackContext context)
        {
            OnRotatePerformed?.Invoke(context.ReadValue<float>());
        }

        private void OnRotateCanceled_Internal(InputAction.CallbackContext context)
        {
            OnRotateCanceled?.Invoke(context.ReadValue<float>());
        }

        public event Action<Vector2> OnMoveStarted;

        public event Action<Vector2> OnMovePerformed;

        public event Action<Vector2> OnMoveCanceled;

        public event Action<float> OnRotateStarted;

        public event Action<float> OnRotatePerformed;

        public event Action<float> OnRotateCanceled;

        public bool IsEnabled { get; private set; }
    }
}
