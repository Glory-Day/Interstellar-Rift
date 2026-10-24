using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// The base class for <see cref="ScriptableObject"/> assets that create a specific local service for installation.
    /// </summary>
    public abstract class LocalServiceFactoryAsset : ScriptableObject, ILocalServiceFactory
    {
        #region SERIALIZABLE PROPERTY API

        /// <summary>
        /// Gets the mode in which the local service created by this asset is installed.
        /// </summary>
        [Title("Service Installation")]
        [ReadOnly]
        [ShowInInspector]
        [PropertyTooltip("The mode in which the local service created by this asset is installed.")]
        public abstract InstallationMode Mode { get; }

        #endregion

        /// <inheritdoc/>
        public abstract LocalService Create(ServiceResolver resolver);
    }
}

