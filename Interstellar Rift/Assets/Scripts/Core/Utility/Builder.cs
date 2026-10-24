using System;
using Console = GloryDay.Debug.Console;

namespace Core.Utility
{
    /// <summary>
    /// Provides a base implementation of <see cref="IBuilder{T}"/> that validates the required
    /// components before creating an instance.
    /// </summary>
    /// <typeparam name="TResult">The type of the instance to build.</typeparam>
    /// <remarks>
    /// <para>
    /// <see cref="Build"/> runs a <see cref="BuildPipeline{TResult}"/> that invokes <see cref="Validate"/>
    /// before <see cref="Create"/>, so that an instance is never created while a required component
    /// is missing. Derived classes implement the validation in <see cref="Validate"/> and the creation
    /// of the instance in <see cref="Create"/>.
    /// </para>
    /// </remarks>
    public abstract class Builder<TResult> : IBuilder<TResult>, IDisposable
    {
        private BuildPipeline<TResult> _pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder{TResult}"/> class.
        /// </summary>
        protected Builder()
        {
            _pipeline = new BuildPipeline<TResult>(Validate, Create);
        }

        /// <summary>
        /// Releases the build pipeline, so that this builder can no longer build instances.
        /// </summary>
        public void Dispose()
        {
            Console.LogProgress();

            _pipeline?.Dispose();
            _pipeline = null;
        }

        /// <summary>
        /// Verifies that all the required components have been specified.
        /// </summary>
        /// <remarks>
        /// Implementations throw a <see cref="Exceptions.MissingBuildComponentException"/>
        /// when a required component has not been specified.
        /// </remarks>
        protected abstract void Validate();

        /// <summary>
        /// Creates an instance of <typeparamref name="TResult"/> from the specified components.
        /// </summary>
        /// <returns>The created instance of <typeparamref name="TResult"/>.</returns>
        /// <remarks>
        /// This method is invoked only after <see cref="Validate"/> has completed without throwing,
        /// so implementations can assume that all the required components have been specified.
        /// </remarks>
        protected abstract TResult Create();

        /// <inheritdoc/>
        /// <exception cref="Exceptions.MissingBuildComponentException">
        /// Thrown when a required component has not been specified.
        /// </exception>
        public TResult Build()
        {
            Console.LogProgress();

            if (_pipeline == null)
            {
                throw new ObjectDisposedException(nameof(Builder<TResult>));
            }

            return _pipeline.Run();
        }
    }
}
