namespace Core.Utility
{
    /// <summary>
    /// Defines methods for registering and retrieving values by key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IRegistry<in TKey, TValue>
    {
        /// <summary>
        /// Registers the given value with the given key.
        /// </summary>
        /// <param name="key">The key to register.</param>
        /// <param name="value">The value to register.</param>
        public void Register(TKey key, TValue value);

        /// <summary>
        /// Unregisters the value registered with the given key.
        /// </summary>
        /// <param name="key">The key of the value to unregister.</param>
        public void Unregister(TKey key);

        /// <summary>
        /// Determines whether a value is registered with the given key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns><see langword="true"/> if a value is registered with the given key; otherwise, <see langword="false"/>.</returns>
        public bool ContainsKey(TKey key);

        /// <summary>
        /// Gets the value registered with the given key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value registered with the given key.</returns>
        public TValue Fetch(TKey key);

        /// <summary>
        /// Attempts to get the value registered with the given key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns <see langword="true"/>, contains the value registered with the given key;
        /// otherwise, the default value for the type of <paramref name="value"/>.
        /// </param>
        /// <returns><see langword="true"/> if a value is registered with the given key; otherwise, <see langword="false"/>.</returns>
        public bool TryFetch(TKey key, out TValue value);
    }
}
