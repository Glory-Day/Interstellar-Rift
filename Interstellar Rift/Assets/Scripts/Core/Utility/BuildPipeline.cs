using System;
using Core.Utility.Extensions;
using Console = GloryDay.Debug.Console;

namespace Core.Utility
{
    /// <summary>
    /// Runs the build process of an instance of <typeparamref name="T"/>, which validates
    /// the required components before creating the instance.
    /// </summary>
    /// <typeparam name="TResult">The type of the instance to build.</typeparam>
    /// <remarks>
    /// <para>
    /// This class allows builders that cannot share a common base class, such as <see cref="Builder{T}"/>
    /// and the builders derived from a local service, to share the same build process through composition.
    /// </para>
    /// </remarks>
    public class BuildPipeline<TResult> : IDisposable
    {
        private Action _validator;
        private Func<TResult> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildPipeline{TResult}"/> class
        /// with the specified validator and factory.
        /// </summary>
        /// <param name="validator">
        /// The action that verifies that all the required components have been specified,
        /// throwing an exception if any of them is missing.
        /// </param>
        /// <param name="factory">The function that creates an instance of <typeparamref name="TResult"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="validator"/> or <paramref name="factory"/> is <see langword="null"/>.
        /// </exception>
        public BuildPipeline(Action validator, Func<TResult> factory)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Releases the validator and the factory, so that the pipeline can no longer be run.
        /// </summary>
        public void Dispose()
        {
            _validator = null;
            _factory = null;
        }

        /// <summary>
        /// Validates the required components and creates an instance of <typeparamref name="TResult"/>.
        /// </summary>
        /// <returns>The created instance of <typeparamref name="TResult"/>.</returns>
        /// <remarks>
        /// The factory is invoked only after the validator has completed without throwing.
        /// Any exception thrown by the validator propagates to the caller.
        /// </remarks>
        public TResult Run()
        {
            Console.LogProgress();

            if (_validator == null || _factory == null)
            {
                throw new ObjectDisposedException(nameof(BuildPipeline<TResult>));
            }

            _validator.Invoke();

            var result = _factory.Invoke();

            Console.LogSuccess(
                $"{typeof(TResult).Name.ToNicifyPascalCase().ToBoldStyle()} is built."
            );

            return result;
        }
    }
}
