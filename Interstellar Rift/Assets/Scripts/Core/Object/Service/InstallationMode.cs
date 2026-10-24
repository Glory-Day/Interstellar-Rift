namespace Core.Object.Service
{
    /// <summary>
    /// Specifies how a local service created by a <see cref="LocalServiceFactoryAsset"/> is installed.
    /// </summary>
    /// <remarks>
    /// A service can depend on another service installed in the same mode only if that service is installed before it.
    /// </remarks>
    public enum InstallationMode
    {
        /// <summary>
        /// The service is installed manually by <see cref="ServiceResolverFactory"/>.
        /// It can depend only on other manually installed services.
        /// </summary>
        /// <remarks>
        /// Specify this mode only for services whose installation is implemented directly in <see cref="ServiceResolverFactory"/>.
        /// </remarks>
        Manual,

        /// <summary>
        /// The service is installed automatically.
        /// It can depend on manually installed services and other automatically installed services.
        /// </summary>
        Automatic,

        /// <summary>
        /// The service is installed lazily.
        /// It can depend on manually and automatically installed services, services that depend on the Unity API,
        /// and other lazily installed services.
        /// </summary>
        Lazy
    }
}
