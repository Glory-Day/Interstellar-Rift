using System;
using System.Collections.Generic;
using Core.Utility.Attributes;
using Core.Utility.Extensions;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    /// <summary>
    /// A registrar that creates local services from the assets waiting in the <see cref="LocalServiceInstallationQueue"/>
    /// and registers them in the service container.
    /// </summary>
    /// <remarks>
    /// Each service is registered under its own type and its base types,
    /// except those marked with <see cref="ExcludeFromInstallationAttribute"/>.
    /// Registering a service under a type that is already registered is treated as an error.
    /// </remarks>
    public class LocalServiceRegistry
    {
        private readonly LocalServiceInstallationQueue _queue;
        private readonly Dictionary<Type, LocalService> _container;
        private readonly ServiceResolver _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServiceRegistry"/> class with the specified queue, container, and resolver.
        /// </summary>
        /// <param name="queue">The queue of assets waiting for installation.</param>
        /// <param name="container">The container in which the installed local services are registered by type.</param>
        /// <param name="resolver">The resolver used to look up the services that the installed local services depend on.</param>
        public LocalServiceRegistry(LocalServiceInstallationQueue queue,
                                    Dictionary<Type, LocalService> container,
                                    ServiceResolver resolver)
        {
            Console.LogProgress();

            _queue = queue;
            _container = container;
            _resolver = resolver;
        }

        /// <summary>
        /// Attempts to take the manual installation asset of the specified type, create its local service, and register the service.
        /// </summary>
        /// <remarks>
        /// This method applies only to manual installation assets.
        /// </remarks>
        /// <typeparam name="T">The type of the asset to take.</typeparam>
        /// <param name="action">The delegate that creates the local service from the taken asset.</param>
        /// <returns><see langword="true"/> if a matching asset was found and its service was registered; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when a service is already registered under one of the types of the created service.</exception>
        public bool TryRegisterByType<T>(Func<T, LocalService> action) where T : LocalServiceFactoryAsset
        {
            Console.LogProgress();

            if (_queue.TryTakeManualInstallationAsset<T>(out var factory) is false)
            {
                return false;
            }

            var service = action.Invoke(factory);

            Register(service);

            return true;
        }

        /// <summary>
        /// Creates and registers the local services of all automatic installation assets in queue order.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when a service is already registered under one of the types of a created service.</exception>
        public void RegisterAllAsAutomatic()
        {
            Console.LogProgress();

            while (_queue.TryTakeAutomaticInstallationAsset(out var factory))
            {
                var service = factory.Create(_resolver);

                Register(service);
            }
        }

        /// <summary>
        /// Creates and registers the local services of all lazy installation assets in queue order.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when a service is already registered under one of the types of a created service.</exception>
        public void RegisterAllAsLazy()
        {
            while (_queue.TryTakeLazyInstallationAsset(out var factory))
            {
                var service = factory.Create(_resolver);

                Register(service);
            }
        }

        /// <summary>
        /// Registers the specified service in the container under its own type and its non-excluded base types.
        /// </summary>
        /// <param name="service">The service to register.</param>
        /// <exception cref="ArgumentException">Thrown when a service is already registered under one of the types.</exception>
        private void Register(LocalService service)
        {
            foreach (var type in service.GetType().GetHierarchyWithoutExcluded<ExcludeFromInstallationAttribute>())
            {
                _container.Add(type, service);
            }

            Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is registered.");
        }
    }
}
