using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// Represents a <see cref="ScriptableObject"/> asset that installs one or more services into a dependency injection container.
    /// </summary>
    public abstract class ServiceInstallerAsset : ScriptableObject
    {
        /// <summary>
        /// Installs the services this asset represents into the container.
        /// </summary>
        /// <param name="builder">The container builder used to register services.</param>
        public abstract void Install(IContainerBuilder builder);
    }
}
