namespace Core.Object.Service
{
    /// <summary>
    /// Defines a method for resolving global services.
    /// </summary>
    /// <remarks>
    /// In this interface, a global service refers to any object registered with VContainer
    /// for global use. This includes not only instances of <see cref="GlobalService"/> but also
    /// objects that depend on UnityEngine, such as a <see cref="UnityEngine.Camera"/>.
    /// </remarks>
    public interface IGlobalServiceResolver
    {
        /// <summary>
        /// Resolves the global service of the specified type registered with VContainer.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the global service to resolve. This type is not constrained to
        /// <see cref="GlobalService"/>, so that objects that depend on Unity API can also be resolved.
        /// </typeparam>
        /// <returns>The global service of type <typeparamref name="TService"/>.</returns>
        /// <remarks>
        /// Implementations throw an exception if no global service of type
        /// <typeparamref name="TService"/> is registered.
        /// </remarks>
        public TService GetGlobalService<TService>();
    }
}
