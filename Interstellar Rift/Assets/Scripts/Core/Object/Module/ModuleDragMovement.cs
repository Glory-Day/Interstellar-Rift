using Core.Object.Service;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleDragMovement : LocalServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [SerializeField] [Range(0f, 10f)] private float speed = 5f;

        #endregion

        private Camera _camera;
        private Vector3 _offset;

        private bool _isMoving;

        public override void Initialize()
        {
            Console.LogProgress();

            _camera = Resolver.GetGlobalService<Camera>();

            base.Initialize();
        }

        public void BeginMoving(Vector2 position)
        {
            var point = _camera.ScreenToWorldPoint(position);
            point.z = 0f;

            _offset = transform.position - point;

            _isMoving = true;
        }

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

        public void EndMoving()
        {
            _isMoving = false;
        }

        public void Rotate(Vector3 position)
        {
            var direction = (Vector2)(position - transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle);

            transform.rotation = speed > 0f ? Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed) : rotation;
        }
    }
}
