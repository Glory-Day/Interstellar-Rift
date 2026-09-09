using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// Represents a <see cref="ScriptableObject"/> asset that installs one or more services into a dependency injection container.
    /// </summary>
    public abstract class ServiceInstallerAsset : ScriptableObject, IServiceInstallable
    {
        #region SERIALIZABLE FIELD API

        [Title("Assets")]
        [SerializeField] private List<GlobalServiceRegistrarAsset> assets;

        #endregion

        /// <summary>
        /// Installs the services this asset represents into the container.
        /// </summary>
        /// <param name="builder">The container builder used to register services.</param>
        public virtual void Install(IContainerBuilder builder)
        {
            var count = assets.Count;
            for (var i = 0; i < count; i++)
            {
                var asset = assets[i];

                asset.Register(builder);
            }
        }
    }
}
