using Core.Object.Service;

namespace Core.Object.Module
{
    /// <summary>
    /// Initializes all local services registered with a <see cref="ServiceResolver"/> for a single game object.
    /// </summary>
    public class ModuleLocalServiceInitializer : LocalServiceBehaviour.Friend, IInitializable
    {
        private readonly ServiceResolver _resolver;

        /// <param name="resolver">The <see cref="ServiceResolver"/> whose local services this instance initializes.</param>
        public ModuleLocalServiceInitializer(ServiceResolver resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// Initializes every registered local service by assigning it a <see cref="ServiceResolver"/> and calling <see cref="LocalServiceBehaviour.Initialize"/> on it.
        /// </summary>
        public void Initialize()
        {
            var services = _resolver.GetAllLocalServices();
            var count = services.Count;

            for (var i = 0; i < count; i++)
            {
                // Resolver must be assigned before it is initialized, since implementations may use it during initialization.
                SetServiceResolver(services[i], _resolver);

                services[i].Initialize();
            }
        }
    }
}
