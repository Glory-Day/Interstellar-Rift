using Core.Object.Service;
using Cysharp.Threading.Tasks;
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

        private ModuleConnector _moduleConnector;

        private ModuleDragMovement _moduleDragMovement;

        private MouseButtonEventDispatcher _mouseButtonEventDispatcher;

        #endregion

        public ModuleDragEventHandler(ModuleConnector moduleConnector,
                                      ModuleDragMovement moduleDragMovement,
                                      MouseButtonEventDispatcher mouseButtonEventDispatcher)
        {
            Console.LogProgress();

            _moduleConnector = moduleConnector;

            _moduleDragMovement = moduleDragMovement;

            _mouseButtonEventDispatcher = mouseButtonEventDispatcher;
            _mouseButtonEventDispatcher.OnMouseButtonPressed += Disconnect;
            _mouseButtonEventDispatcher.OnMouseButtonPressed += StartMoving;
            _mouseButtonEventDispatcher.OnMouseButtonPressed += StartSearching;
            _mouseButtonEventDispatcher.OnMouseButtonDragged += Move;
            _mouseButtonEventDispatcher.OnMouseButtonReleased += StopMoving;
            _mouseButtonEventDispatcher.OnMouseButtonReleased += StopSearching;
            _mouseButtonEventDispatcher.OnMouseButtonReleased += TryConnection;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _moduleConnector = null;

            _moduleDragMovement = null;

            _mouseButtonEventDispatcher.OnMouseButtonPressed -= Disconnect;
            _mouseButtonEventDispatcher.OnMouseButtonPressed -= StartMoving;
            _mouseButtonEventDispatcher.OnMouseButtonPressed -= StartSearching;
            _mouseButtonEventDispatcher.OnMouseButtonDragged -= Move;
            _mouseButtonEventDispatcher.OnMouseButtonReleased -= StopMoving;
            _mouseButtonEventDispatcher.OnMouseButtonReleased -= StopSearching;
            _mouseButtonEventDispatcher.OnMouseButtonReleased -= TryConnection;
            _mouseButtonEventDispatcher = null;
        }

        /// <summary>
        /// Forwards the pointer's screen position to <see cref="ModuleDragMovement.StartMoving"/> to start dragging.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse press.</param>
        private void StartMoving(PointerEventData eventData)
        {
            if (_moduleConnector.IsConnecting)
            {
                return;
            }

            _moduleDragMovement.StartMoving(eventData.position);
        }

        /// <summary>
        /// Forwards the pointer's screen position to <see cref="ModuleDragMovement.Move"/> to continue dragging.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse drag.</param>
        private void Move(PointerEventData eventData)
        {
            if (_moduleConnector.IsConnecting)
            {
                return;
            }

            _moduleDragMovement.Move(eventData.position);
        }

        /// <summary>
        /// Stops dragging via <see cref="ModuleDragMovement.StopMoving"/>.
        /// </summary>
        /// <param name="eventData">The pointer event data from the mouse release.</param>
        private void StopMoving(PointerEventData eventData)
        {
            _moduleDragMovement.StopMoving();
        }

        private void StartSearching(PointerEventData eventData)
        {
            _moduleConnector.StartSearching();
        }

        private void StopSearching(PointerEventData eventData)
        {
            _moduleConnector.StopSearching();
        }

        private void TryConnection(PointerEventData eventData)
        {
            _moduleConnector.TryConnect().Forget();
        }

        private void Disconnect(PointerEventData eventData)
        {
            _moduleConnector.Disconnect();
        }
    }
}
