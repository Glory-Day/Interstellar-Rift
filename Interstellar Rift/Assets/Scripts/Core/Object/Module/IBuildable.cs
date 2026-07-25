namespace Core.Object.Module
{
    /// <summary>
    /// Represents an object that can build and return an instance of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of instance this builder produces.</typeparam>
    public interface IBuildable<out T>
    {
        /// <summary>
        /// Assembles and returns the built instance of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The fully constructed instance.</returns>
        T Build();
    }
}
