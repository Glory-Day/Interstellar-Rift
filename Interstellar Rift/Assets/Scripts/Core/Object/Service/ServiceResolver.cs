using System.Collections.Generic;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// Resolves registered local services from a serialized list and global services registered with DI via VContainer.
    /// </summary>
    public class ServiceResolver : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [Tooltip("List of local services to register for use by this game object.")]
        [SerializeField] private List<LocalServiceBehaviour> services;

        #endregion

        private IObjectResolver _resolver;

        [Inject]
        private void Install(IObjectResolver resolver)
        {
            Console.LogProgress();

            _resolver = resolver;
        }

        /// <summary>
        /// Linearly searches the serialized local service list for the first entry assignable to <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The local service type to find, derived from <see cref="LocalServiceBehaviour"/>.</typeparam>
        /// <returns>The matching service instance, or <c>null</c> if none is found.</returns>
        public TService GetLocalService<TService>() where TService : LocalServiceBehaviour
        {
            Console.LogProgress();

            var count = services.Count;
            for (var i = 0; i < count; i++)
            {
                if (services[i] is TService)
                {
                    return (TService)services[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Resolves a global service of type <typeparamref name="TService"/> from the injected VContainer <see cref="IObjectResolver"/>.
        /// </summary>
        /// <typeparam name="TService">The registered service type to resolve.</typeparam>
        /// <returns>The resolved service instance.</returns>
        public TService GetGlobalService<TService>()
        {
            Console.LogProgress();

            return _resolver.Resolve<TService>();
        }

        /// <summary>
        /// Gets the full list of registered local services.
        /// </summary>
        /// <returns>A read-only view of the serialized list of local services.</returns>
        public IReadOnlyList<LocalServiceBehaviour> GetAllLocalServices()
        {
            return services;
        }
    }
}
