namespace Core.Object.Service
{
    /// <summary>
    /// Defines a method for creating local services.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A factory is responsible for obtaining the dependencies of a local service and supplying them
    /// to its constructor. The dependencies are obtained through the <see cref="ServiceResolver"/>
    /// of the module in which the created service is installed.
    /// </para>
    /// <para>
    /// A factory may also decide at creation time which concrete type of local service to create.
    /// Because the decision is made only within the factory, the clients of the created service
    /// do not need to know its concrete type.
    /// </para>
    /// </remarks>
    public interface ILocalServiceFactory
    {
        /// <summary>
        /// Creates a new local service, obtaining the services required for its creation
        /// from <paramref name="resolver"/>.
        /// </summary>
        /// <param name="resolver">
        /// The resolver of the module in which the created service is installed, used to obtain
        /// the services on which the created service depends.
        /// </param>
        /// <returns>The newly created local service, whose concrete type is determined by the factory.</returns>
        /// <remarks>
        /// Local services are installed in a fixed order. Within this method, only the local services
        /// installed before the service being created can be obtained from <paramref name="resolver"/>;
        /// requesting a local service that has not yet been installed causes a
        /// <see cref="Core.Utility.Exceptions.MissingRequiredLocalServiceException"/>.
        /// All global services can be obtained regardless of the installation order.
        /// </remarks>
        public LocalService Create(ServiceResolver resolver);
    }
}
