using System;
using UnityEngine;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    /// <summary>
    /// A global service that exposes Unity's <c>Update</c>, <c>FixedUpdate</c>, and <c>LateUpdate</c> messages as events,
    /// allowing non-<see cref="MonoBehaviour"/> classes to subscribe to per-frame updates.
    /// </summary>
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

        /// <summary>
        /// Occurs every frame, mirroring Unity's <c>Update</c> message.
        /// </summary>
        public event Action OnUpdate;

        /// <summary>
        /// Occurs every fixed-timestep physics frame, mirroring Unity's <c>FixedUpdate</c> message.
        /// </summary>
        public event Action OnFixedUpdate;

        /// <summary>
        /// Occurs every frame after all <see cref="OnUpdate"/> subscribers have run, mirroring Unity's <c>LateUpdate</c> message.
        /// </summary>
        public event Action OnLateUpdate;
    }
}
