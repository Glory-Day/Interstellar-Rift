using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to move a module while it is being dragged.
    /// </summary>
    public class ModuleDragMovement : LocalService
    {
        #region GLOBAL SERVICE API

        private Camera _camera;

        #endregion

        #region LOCAL SERVICE API

        private Transform _transform;

        #endregion

        private Vector3 _offset;
        private bool _isMoving;

        public ModuleDragMovement(Camera camera, Transform transform)
        {
            Console.LogProgress();

            _camera = camera;

            _transform = transform;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _camera = null;

            _transform = null;
        }

        /// <summary>
        /// Starts dragging the module, caching the offset between the module's position and the given screen position.
        /// </summary>
        /// <param name="position">The screen position where the drag started.</param>
        public void StartMoving(Vector2 position)
        {
            var point = _camera.ScreenToWorldPoint(position);
            point.z = 0f;

            _offset = _transform.position - point;

            _isMoving = true;
        }

        /// <summary>
        /// Moves the module to follow the given screen position, preserving the captured offset.
        /// Does nothing if the module is not currently being dragged.
        /// </summary>
        /// <param name="position">The current screen position of the drag.</param>
        public void Move(Vector2 position)
        {
            if (_isMoving is false)
            {
                return;
            }

            var point = _camera.ScreenToWorldPoint(position);
            point.z = 0f;

            _transform.position = point + _offset;
        }

        /// <summary>
        /// Stops dragging the module.
        /// </summary>
        public void StopMoving()
        {
            _isMoving = false;
        }
    }
}
