using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Utility.Input
{
    /// <summary>
    /// An <see cref="IGameInputActions"/> that wraps the generated <see cref="ModuleInputActions"/> "Default" action map,
    /// exposing module click and drag input as subscribable events.
    /// </summary>
    public class DefaultModuleInputActions : IGameInputActions
    {
        private readonly ModuleInputActions _inputActions;

        /// <param name="inputActions">The generated <see cref="ModuleInputActions"/> to read module input from.</param>
        public DefaultModuleInputActions(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <summary>
        /// Forwards the click action's <c>started</c> phase to <see cref="OnClickStarted"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the click input value.</param>
        private void OnClickStarted_Internal(InputAction.CallbackContext context)
        {
            OnClickStarted?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the click action's <c>performed</c> phase to <see cref="OnClickPerformed"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the click input value.</param>
        private void OnClickPerformed_Internal(InputAction.CallbackContext context)
        {
            OnClickPerformed?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the click action's <c>canceled</c> phase to <see cref="OnClickCanceled"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the click input value.</param>
        private void OnClickCanceled_Internal(InputAction.CallbackContext context)
        {
            OnClickCanceled?.Invoke(context.ReadValue<float>());
        }

        /// <summary>
        /// Forwards the dragging action's <c>started</c> phase to <see cref="OnDraggingStarted"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the drag input value.</param>
        private void OnDraggingStarted_Internal(InputAction.CallbackContext context)
        {
            OnDraggingStarted?.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Forwards the dragging action's <c>performed</c> phase to <see cref="OnDraggingPerformed"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the drag input value.</param>
        private void OnDraggingPerformed_Internal(InputAction.CallbackContext context)
        {
            OnDraggingPerformed?.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Forwards the dragging action's <c>canceled</c> phase to <see cref="OnDraggingCanceled"/>.
        /// </summary>
        /// <param name="context">The callback context carrying the drag input value.</param>
        private void OnDraggingCanceled_Internal(InputAction.CallbackContext context)
        {
            OnDraggingCanceled?.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// Occurs when the click input starts, passing the current click value.
        /// </summary>
        public event Action<float> OnClickStarted;

        /// <summary>
        /// Occurs every frame the click input is being performed, passing the current click value.
        /// </summary>
        public event Action<float> OnClickPerformed;

        /// <summary>
        /// Occurs when the click input is canceled, passing the click value at cancellation.
        /// </summary>
        public event Action<float> OnClickCanceled;

        /// <summary>
        /// Occurs when the dragging input starts, passing the current drag delta.
        /// </summary>
        public event Action<Vector2> OnDraggingStarted;

        /// <summary>
        /// Occurs every frame the dragging input is being performed, passing the current drag delta.
        /// </summary>
        public event Action<Vector2> OnDraggingPerformed;

        /// <summary>
        /// Occurs when the dragging input is canceled, passing the drag delta at cancellation.
        /// </summary>
        public event Action<Vector2> OnDraggingCanceled;

        /// <inheritdoc/>
        public bool IsEnabled { get; private set; }
    }
}
