using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Utility.Input
{
    /// <summary>
    /// An <see cref="IGameInputActions"/> that wraps the generated <see cref="ModuleInputActions"/> "Booster" action map,
    /// exposing booster move and rotate input as subscribable events.
    /// </summary>
    public class BoosterModuleInputActions : IGameInputActions
    {
        private readonly ModuleInputActions _inputActions;

        /// <param name="inputActions">The generated <see cref="ModuleInputActions"/> to read booster input from.</param>
        public BoosterModuleInputActions(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <summary>
        /// Forwards the move action's <c>started</c> phase to <see cref="OnMoveStarted"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the move input value.</param>
        private void OnMoveStarted_Internal(InputAction.CallbackContext context)
        {
            OnMoveStarted?.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Forwards the move action's <c>performed</c> phase to <see cref="OnMovePerformed"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the move input value.</param>
        private void OnMovePerformed_Internal(InputAction.CallbackContext context)
        {
            OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Forwards the move action's <c>canceled</c> phase to <see cref="OnMoveCanceled"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the move input value.</param>
        private void OnMoveCanceled_Internal(InputAction.CallbackContext context)
        {
            OnMoveCanceled?.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Forwards the rotate action's <c>started</c> phase to <see cref="OnRotateStarted"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the rotate input value.</param>
        private void OnRotateStarted_Internal(InputAction.CallbackContext context)
        {
            OnRotateStarted?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the rotate action's <c>performed</c> phase to <see cref="OnRotatePerformed"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the rotate input value.</param>
        private void OnRotatePerformed_Internal(InputAction.CallbackContext context)
        {
            OnRotatePerformed?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the rotate action's <c>canceled</c> phase to <see cref="OnRotateCanceled"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the rotate input value.</param>
        private void OnRotateCanceled_Internal(InputAction.CallbackContext context)
        {
            OnRotateCanceled?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Occurs when the move input starts, passing the current move vector.
        /// </summary>
        public event Action<Vector2> OnMoveStarted;

        /// <summary>
        /// Occurs every frame the move input is being performed, passing the current move vector.
        /// </summary>
        public event Action<Vector2> OnMovePerformed;

        /// <summary>
        /// Occurs when the move input is canceled, passing the move vector at cancellation.
        /// </summary>
        public event Action<Vector2> OnMoveCanceled;

        /// <summary>
        /// Occurs when the rotate input starts, passing the current rotate value.
        /// </summary>
        public event Action<float> OnRotateStarted;

        /// <summary>
        /// Occurs every frame the rotate input is being performed, passing the current rotate value.
        /// </summary>
        public event Action<float> OnRotatePerformed;

        /// <summary>
        /// Occurs when the rotate input is canceled, passing the rotate value at cancellation.
        /// </summary>
        public event Action<float> OnRotateCanceled;

        /// <inheritdoc/>
        public bool IsEnabled { get; private set; }
    }
}
