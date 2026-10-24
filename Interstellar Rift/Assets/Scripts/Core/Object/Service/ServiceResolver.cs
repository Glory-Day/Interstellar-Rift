using System;
using System.Collections.Generic;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    /// <summary>
    /// Resolves the local services installed in a single module and the global services
    /// registered with VContainer, and supplies them to the client objects that depend on them.
    /// </summary>
    /// <remarks>
    /// Each module has its own <see cref="ServiceResolver"/>. Local services are resolved only
    /// from those installed in the module that owns this resolver, whereas global services are
    /// resolved from VContainer.
    /// </remarks>
    public class ServiceResolver : IGlobalServiceResolver, ILocalServiceResolver
    {
        private readonly IReadOnlyDictionary<Type, LocalService> _services;
        private readonly IObjectResolver _resolver;

        /// <param name="resolver">The VContainer resolver from which global services are resolved.</param>
        /// <param name="services">
        /// The local services installed in the module that owns this resolver,
        /// keyed by the type under which each service is installed.
        /// </param>
        public ServiceResolver(IObjectResolver resolver, IReadOnlyDictionary<Type, LocalService> services)
        {
            Console.LogProgress();

            _resolver = resolver;
            _services = services;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// The local service is looked up by its exact installed type.
        /// A local service installed under a type derived from <typeparamref name="TService"/> is not matched.
        /// </remarks>
        /// <exception cref="MissingRequiredLocalServiceException">
        /// Thrown when no local service is installed under <typeparamref name="TService"/> in the module.
        /// </exception>
        public TService GetLocalService<TService>() where TService : LocalService
        {
            if (_services.TryGetValue(typeof(TService), out var service) == false)
            {
                throw new MissingRequiredLocalServiceException(typeof(TService).Name.ToNicifyPascalCase());
            }

            return (TService)service;
        }

        /// <inheritdoc/>
        /// <exception cref="VContainerException">
        /// Thrown when no global service of type <typeparamref name="TService"/> is registered with VContainer.
        /// </exception>
        public TService GetGlobalService<TService>()
        {
            Console.LogProgress();

            return _resolver.Resolve<TService>();
        }
    }
}
