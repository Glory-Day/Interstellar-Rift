using System;
using UnityEngine;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    public class UpdateEventHandler : MonoBehaviour
    {
        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            Console.LogProgress();

            OnUpdate = null;
            OnFixedUpdate = null;
            OnLateUpdate = null;
        }

        public event Action OnUpdate;

        public event Action OnFixedUpdate;

        public event Action OnLateUpdate;
    }
}
