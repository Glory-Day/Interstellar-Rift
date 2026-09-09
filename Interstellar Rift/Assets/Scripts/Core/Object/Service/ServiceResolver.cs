using System.Collections.Generic;
using Core.Utility.Exception;
using Core.Utility.Extension;
using GloryDay.Debug;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// Resolves registered local services from a serialized list and global services registered with DI via VContainer.
    /// </summary>
    public class ServiceResolver
    {
        private readonly IReadOnlyList<LocalService> _services;
        private readonly IObjectResolver _resolver;

        public ServiceResolver(IObjectResolver resolver, IReadOnlyList<LocalService> services)
        {
            Console.LogProgress();

            _resolver = resolver;
            _services = services;
        }

        /// <summary>
        /// Linearly searches the serialized local service list for the first entry assignable to <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The local service type to find, derived from <see cref="LocalService"/>.</typeparam>
        /// <returns>The matching service instance, or <c>null</c> if none is found.</returns>
        public TService GetLocalService<TService>() where TService : LocalService
        {
            Console.LogProgress();

            var count = _services.Count;
            for (var i = 0; i < count; i++)
            {
                if (_services[i] is TService)
                {
                    return (TService)_services[i];
                }
            }

            throw new MissingRequiredLocalServiceException(typeof(TService).Name.ToNicifyPascalCase());
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
    }
}
