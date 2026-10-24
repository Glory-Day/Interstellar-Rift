using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// Defines a method for registering a created global service.
    /// </summary>
    public interface IGlobalServiceRegistrable
    {
        /// <summary>
        /// Registers the service with VContainer so that it can be used globally.
        /// </summary>
        /// <param name="builder">The builder used to register the service with VContainer.</param>
        public void Register(IContainerBuilder builder);
    }
}
