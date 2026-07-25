using Core.Object.Service;
using UnityEngine;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// Coordinates module dragging by wiring mouse button events to the module's movement, slot search, and visual effect services.
    /// </summary>
    public class ModuleDragEventHandler : LocalServiceBehaviour
    {
        #region SERVICE FIELD API

        private ModuleConnector _connector;
        private ModuleSearcher _searcher;
        private ModuleDragMovement _movement;
        private ModuleDragEffector _effector;

        private MouseButtonEventHandler _handler;

        #endregion

        /// <inheritdoc/>
        public override void Initialize()
        {
            Console.LogProgress();

            _connector = Resolver.GetLocalService<ModuleConnector>();
            _searcher = Resolver.GetLocalService<ModuleSearcher>();
            _movement = Resolver.GetLocalService<ModuleDragMovement>();
            _effector = Resolver.GetLocalService<ModuleDragEffector>();

            _handler = Resolver.GetLocalService<MouseButtonEventHandler>();
            _handler.OnMouseButtonPressed += BeginMoving;
            _handler.OnMouseButtonPressed += Search;
            _handler.OnMouseButtonDragged += Move;
            _handler.OnMouseButtonDragged += Search;
            _handler.OnMouseButtonReleased += EndMoving;
            _handler.OnMouseButtonReleased += EndVisualEffect;

            base.Initialize();
        }

        private void OnDestroy()
        {
            Console.LogProgress();

            _handler.OnMouseButtonPressed -= BeginMoving;
            _handler.OnMouseButtonPressed -= Search;
            _handler.OnMouseButtonDragged -= Move;
            _handler.OnMouseButtonDragged -= Search;
            _handler.OnMouseButtonReleased -= EndMoving;
            _handler.OnMouseButtonReleased -= EndVisualEffect;
        }

        /// <summary>
        /// Forwards the pointer's screen position to <see cref="ModuleDragMovement.BeginMoving"/> to start dragging.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse press.</param>
        private void BeginMoving(PointerEventData eventData)
        {
            _movement.BeginMoving(eventData.position);
        }

        /// <summary>
        /// Forwards the pointer's screen position to <see cref="ModuleDragMovement.Move"/> to continue dragging.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse drag.</param>
        private void Move(PointerEventData eventData)
        {
            _movement.Move(eventData.position);
        }

        /// <summary>
        /// Stops dragging via <see cref="ModuleDragMovement.EndMoving"/>.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse release.</param>
        private void EndMoving(PointerEventData eventData)
        {
            _movement.EndMoving();
        }

        /// <summary>
        /// Searches for the nearest attachable slot and, if one is found, starts or updates the arc visual effect
        /// toward it and rotates the module to face it; otherwise stops the visual effect if it is running.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse press or drag.</param>
        private void Search(PointerEventData eventData)
        {
            var result = _searcher.Search();
            var spawner = _connector.Joint.transform.position;

            if (result.HasValue)
            {
                if (_effector.IsUpdating == false)
                {
                    _effector.StartVisualEffect(spawner, result.Value);
                }
            }
            else
            {
                if (_effector.IsUpdating)
                {
                    _effector.StopVisualEffect();
                }

                return;
            }

            var slot = result.Value.Slot;
            var target = slot.transform.position;

            _effector.UpdateVisualEffect(spawner, result.Value);
            _movement.Rotate(target);
        }

        /// <summary>
        /// Stops the arc visual effect via <see cref="ModuleDragEffector.StopVisualEffect"/>.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse release.</param>
        private void EndVisualEffect(PointerEventData eventData)
        {
            _effector.StopVisualEffect();
        }
    }
}
