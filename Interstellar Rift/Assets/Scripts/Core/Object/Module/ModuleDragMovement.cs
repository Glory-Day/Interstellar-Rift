using Core.Object.Service;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to move a module while it is being dragged.
    /// </summary>
    public class ModuleDragMovement : LocalServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("Rotation speed. If 0, the module rotates instantly instead of smoothly.")]
        [SerializeField] [Range(0f, 10f)] private float speed = 5f;

        #endregion

        #region SERVICE FIELD API

        private Camera _camera;

        #endregion

        private Vector3 _offset;

        private bool _isMoving;

        /// <inheritdoc/>
        public override void Initialize()
        {
            Console.LogProgress();

            _camera = Resolver.GetGlobalService<Camera>();

            base.Initialize();
        }

        /// <summary>
        /// Starts dragging the module, caching the offset between the module's position and the given screen position.
        /// </summary>
        /// <param name="position">The screen position where the drag started.</param>
        public void BeginMoving(Vector2 position)
        {
            var point = _camera.ScreenToWorldPoint(position);
            point.z = 0f;

            _offset = transform.position - point;

            _isMoving = true;
        }

        /// <summary>
        /// Moves the module to follow the given screen position, preserving the captured offset.
        /// Does nothing if the module is not currently being dragged.
        /// </summary>
        /// <param name="position">The current screen position of the drag.</param>
        public void Move(Vector2 position)
        {
            if (_isMoving == false)
            {
                return;
            }

            var point = _camera.ScreenToWorldPoint(position);
            point.z = 0f;

            transform.position = point + _offset;
        }

        /// <summary>
        /// Stops dragging the module.
        /// </summary>
        public void EndMoving()
        {
            _isMoving = false;
        }

        /// <summary>
        /// Rotates the module to face the nearest module found by <see cref="ModuleSearcher"/>, smoothly if speed is greater than zero, or instantly otherwise.
        /// </summary>
        /// <param name="position">The world position of the nearest module found by <see cref="ModuleSearcher"/>.</param>
        public void Rotate(Vector3 position)
        {
            var direction = (Vector2)(position - transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle);

            transform.rotation = speed > 0f ? Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed) : rotation;
        }
    }
}
