using Core.Utility;
using Core.Utility.Attributes;
using GloryDay.Debug;

namespace Core.Object.Service
{
    /// <summary>
    /// Provides a base implementation of <see cref="IBuilder{T}"/> for local services
    /// that build other local services.
    /// </summary>
    /// <typeparam name="TLocalService">The type of the local service to build.</typeparam>
    /// <remarks>
    /// <para>
    /// Because a class cannot derive from both <see cref="LocalService"/> and <see cref="Builder{T}"/>,
    /// this class shares the same build process as <see cref="Builder{T}"/> through
    /// <see cref="BuildPipeline{T}"/>. <see cref="Build"/> invokes <see cref="Validate"/> before
    /// <see cref="Create"/>, so that an instance is never created while a required component is missing.
    /// </para>
    /// <para>
    /// This class is marked with <see cref="ExcludeFromInstallationAttribute"/>, so a service derived
    /// from it cannot be resolved as <see cref="LocalServiceBuilder{TLocalService}"/>.
    /// </para>
    /// </remarks>
    [ExcludeFromInstallation]
    public abstract class LocalServiceBuilder<TLocalService> : LocalService, IBuilder<TLocalService>
        where TLocalService : LocalService
    {
        private BuildPipeline<TLocalService> _pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServiceBuilder{TLocalService}"/> class.
        /// </summary>
        protected LocalServiceBuilder()
        {
            _pipeline = new BuildPipeline<TLocalService>(Validate, Create);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Console.LogProgress();

            _pipeline?.Dispose();
            _pipeline = null;
        }

        /// <summary>
        /// Verifies that all the required components have been specified.
        /// </summary>
        /// <remarks>
        /// Implementations throw a <see cref="Core.Utility.Exceptions.MissingBuildComponentException"/>
        /// when a required component has not been specified.
        /// </remarks>
        protected abstract void Validate();

        /// <summary>
        /// Creates an instance of <typeparamref name="TLocalService"/> from the specified components.
        /// </summary>
        /// <returns>The created instance of <typeparamref name="TLocalService"/>.</returns>
        /// <remarks>
        /// This method is invoked only after <see cref="Validate"/> has completed without throwing,
        /// so implementations can assume that all the required components have been specified.
        /// </remarks>
        protected abstract TLocalService Create();

        /// <inheritdoc/>
        /// <exception cref="Core.Utility.Exceptions.MissingBuildComponentException">
        /// Thrown when a required component has not been specified.
        /// </exception>
        public TLocalService Build()
        {
            Console.LogProgress();

            return _pipeline.Run();
        }
    }
}
