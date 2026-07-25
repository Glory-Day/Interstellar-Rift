namespace Core.Object.Module
{
    /// <summary>
    /// Represents a factory that creates instances of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of instance this factory creates.</typeparam>
    public interface IFactory<out T>
    {
        /// <summary>
        /// Creates and returns a new instance of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The newly created instance.</returns>
        public T Create();
    }
}
