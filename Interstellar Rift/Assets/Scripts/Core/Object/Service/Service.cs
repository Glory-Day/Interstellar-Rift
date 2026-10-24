using System;
using Core.Utility.Attributes;

namespace Core.Object.Service
{
    /// <summary>
    /// Serves as the common base class for all services, both global and local.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A service is a component that is registered with a container and supplied to
    /// the client objects that depend on it. This class defines the type shared by
    /// global services, which are accessible to every client object, and local services,
    /// which are resolved through <see cref="ServiceResolver"/> and accessible only to
    /// the client objects within a single module. The two kinds differ solely in the
    /// range of client objects to which they are accessible; their lifetimes are identical.
    /// </para>
    /// <para>
    /// Every service is disposed through <see cref="Dispose"/> when a transition to
    /// another scene occurs. Client objects must not dispose a service that was supplied
    /// to them, since the service may still be in use by other clients.
    /// </para>
    /// </remarks>
    [ExcludeFromInstallation]
    public abstract class Service : IDisposable
    {
        /// <summary>
        /// Releases the resources held by this service.
        /// </summary>
        /// <remarks>
        /// This method is invoked when a transition to another scene occurs.
        /// Implementations should release every resource acquired during the lifetime
        /// of the service, such as event subscriptions and pending asynchronous operations.
        /// </remarks>
        public abstract void Dispose();
    }
}
