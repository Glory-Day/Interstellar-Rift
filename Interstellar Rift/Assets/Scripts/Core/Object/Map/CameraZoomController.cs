using System;
using Core.Utility.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Core.Object.Map
{
    public class CameraZoomController : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Settings")]
        [SerializeField] private float min;
        [SerializeField] private float max;
        [SerializeField] private float speed;

        #endregion

        private Camera _mainCamera;

        private MapInputActions _actions;

        [Inject]
        private void Install(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        [Inject]
        private void Install(MapInputActions actions)
        {
            _actions = actions;
        }

        private void OnEnable()
        {
            _actions.Camera.Zoom.performed += Zoom;
        }

        private void OnDisable()
        {
            _actions.Camera.Zoom.performed -= Zoom;
        }

        private void Zoom(InputAction.CallbackContext context)
        {
            // Changed mouse horizontal wheel scroll input value.
            var delta = context.ReadValue<float>();

            // Apply the main camera zoom level as the input value.
            var size = _mainCamera.orthographicSize;
            size = Mathf.Clamp(size - delta * speed, min, max);

            if (Mathf.Approximately(size, _mainCamera.orthographicSize))
            {
                return;
            }

            _mainCamera.orthographicSize = size;

            OnZoomChanged?.Invoke(size);
        }

        /// <summary>
        /// Occurs when main camera zoom level changed.
        /// </summary>
        public event Action<float> OnZoomChanged;

        /// <summary>
        /// The zoom level currently set on the main camera.
        /// </summary>
        public float CurrentZoomLevel => _mainCamera.orthographicSize;
    }
}
