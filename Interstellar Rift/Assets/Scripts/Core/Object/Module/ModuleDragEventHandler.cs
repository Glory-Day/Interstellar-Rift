using Core.Object.Service;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// Coordinates module dragging by wiring mouse button events to the module's movement, slot search, and visual effect services.
    /// </summary>
    public class ModuleDragEventHandler : LocalService
    {
        #region LOCAL SERVICE API

        private ModuleSocket _moduleSocket;
        private ModuleSearcher _moduleSearcher;
        private ModuleDragMovement _moduleDragMovement;
        private ModuleDragVisualEffect _moduleDragVisualEffect;
        private MouseButtonEventDispatcher _mouseButtonEventDispatcher;

        #endregion

        public ModuleDragEventHandler(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            _moduleSocket = Resolver.GetLocalService<ModuleSocket>();
            _moduleSearcher = Resolver.GetLocalService<ModuleSearcher>();
            _moduleDragMovement = Resolver.GetLocalService<ModuleDragMovement>();
            _moduleDragVisualEffect = Resolver.GetLocalService<ModuleDragVisualEffect>();

            _mouseButtonEventDispatcher = Resolver.GetLocalService<MouseButtonEventDispatcher>();
            _mouseButtonEventDispatcher.OnMouseButtonPressed += BeginMoving;
            _mouseButtonEventDispatcher.OnMouseButtonPressed += Search;
            _mouseButtonEventDispatcher.OnMouseButtonDragged += Move;
            _mouseButtonEventDispatcher.OnMouseButtonDragged += Search;
            _mouseButtonEventDispatcher.OnMouseButtonReleased += EndMoving;
            _mouseButtonEventDispatcher.OnMouseButtonReleased += EndVisualEffect;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _mouseButtonEventDispatcher.OnMouseButtonPressed -= BeginMoving;
            _mouseButtonEventDispatcher.OnMouseButtonPressed -= Search;
            _mouseButtonEventDispatcher.OnMouseButtonDragged -= Move;
            _mouseButtonEventDispatcher.OnMouseButtonDragged -= Search;
            _mouseButtonEventDispatcher.OnMouseButtonReleased -= EndMoving;
            _mouseButtonEventDispatcher.OnMouseButtonReleased -= EndVisualEffect;

            _moduleSocket = null;
            _moduleSearcher = null;
            _moduleDragMovement = null;
            _moduleDragVisualEffect = null;
            _mouseButtonEventDispatcher = null;
        }

        /// <summary>
        /// Forwards the pointer's screen position to <see cref="ModuleDragMovement.BeginMoving"/> to start dragging.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse press.</param>
        private void BeginMoving(PointerEventData eventData)
        {
            _moduleDragMovement.BeginMoving(eventData.position);
        }

        /// <summary>
        /// Forwards the pointer's screen position to <see cref="ModuleDragMovement.Move"/> to continue dragging.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse drag.</param>
        private void Move(PointerEventData eventData)
        {
            _moduleDragMovement.Move(eventData.position);
        }

        /// <summary>
        /// Stops dragging via <see cref="ModuleDragMovement.EndMoving"/>.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse release.</param>
        private void EndMoving(PointerEventData eventData)
        {
            _moduleDragMovement.EndMoving();
        }

        /// <summary>
        /// Searches for the nearest attachable slot and, if one is found, starts or updates the arc visual effect
        /// toward it and rotates the module to face it; otherwise stops the visual effect if it is running.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse press or drag.</param>
        private void Search(PointerEventData eventData)
        {
            var result = _moduleSearcher.Search();
            var spawner = _moduleSocket.Joint.transform.position;

            if (result.HasValue)
            {
                if (_moduleDragVisualEffect.IsUpdating == false)
                {
                    _moduleDragVisualEffect.StartVisualEffect(spawner, result.Value);
                }
            }
            else
            {
                if (_moduleDragVisualEffect.IsUpdating)
                {
                    _moduleDragVisualEffect.StopVisualEffect();
                }

                return;
            }

            var slot = result.Value.Slot;
            var target = slot.transform.position;

            _moduleDragVisualEffect.UpdateVisualEffect(spawner, result.Value);
            _moduleDragMovement.Rotate(target);
        }

        /// <summary>
        /// Stops the arc visual effect via <see cref="ModuleDragVisualEffect.StopVisualEffect"/>.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse release.</param>
        private void EndVisualEffect(PointerEventData eventData)
        {
            _moduleDragVisualEffect.StopVisualEffect();
        }
    }
}
