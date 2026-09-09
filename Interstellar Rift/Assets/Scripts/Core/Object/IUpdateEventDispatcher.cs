using System;

namespace Core.Object
{
    public interface IUpdateEventDispatcher
    {
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
