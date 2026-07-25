using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// Serves as the base class for services that are available only on specific game objects.
    /// </summary>
    public abstract class LocalServiceBehaviour : MonoBehaviour, IServiceable, IInitializable
    {
        /// <inheritdoc/>
        public virtual void Enable()
        {
            enabled = true;
        }

        /// <inheritdoc/>
        public virtual void Disable()
        {
            enabled = false;
        }

        /// <inheritdoc/>
        public virtual void Initialize()
        {
            Console.LogSuccess($"{GetType().Name} initialized.");
        }

        /// <summary>
        /// The <see cref="ServiceResolver"/> used to resolve local and global services for this instance.
        /// </summary>
        protected ServiceResolver Resolver { get; private set; }

        #region FRIEND CLASS API

        /// <summary>
        /// Restricts write access to <see cref="Resolver"/> to trusted initializer classes that inherit from this type.
        /// </summary>
        public abstract class Friend
        {
            /// <summary>
            /// Assigns the resolved <see cref="ServiceResolver"/> to a <see cref="LocalServiceBehaviour"/> instance.
            /// </summary>
            /// <param name="service">The service instance whose <see cref="Resolver"/> is being set.</param>
            /// <param name="resolver">The resolver to assign.</param>
            protected static void SetServiceResolver(LocalServiceBehaviour service, ServiceResolver resolver)
            {
                service.Resolver = resolver;
            }
        }

        #endregion
    }
}
