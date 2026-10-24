namespace Core.Utility
{
    /// <summary>
    /// Defines a method that builds an instance of <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the instance to build. This type parameter is covariant, so that a builder
    /// of a derived type can be used as a builder of its base type.
    /// </typeparam>
    public interface IBuilder<out TResult>
    {
        /// <summary>
        /// Builds an instance of <typeparamref name="TResult"/> from the components specified to this builder.
        /// </summary>
        /// <returns>The built instance of <typeparamref name="TResult"/>.</returns>
        TResult Build();
    }
}
