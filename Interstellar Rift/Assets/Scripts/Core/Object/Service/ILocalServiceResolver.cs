namespace Core.Object.Service
{
    /// <summary>
    /// Defines a method for looking up local-scoped services.
    /// </summary>
    public interface ILocalServiceResolver
    {
        /// <summary>
        /// Looks up a local-scoped service matching <typeparamref name="TService"/> and provides it if found.
        /// </summary>
        /// <typeparam name="TService">The type of the local service instance to look up.</typeparam>
        /// <returns>The local service instance to provide.</returns>
        public TService GetLocalService<TService>() where TService : LocalService;
    }
}
