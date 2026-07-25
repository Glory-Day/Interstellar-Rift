using System.Collections.Generic;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    /// <summary>
    /// Configures the dependency injection container by installing every registered <see cref="ServiceInstallerAsset"/>.
    /// </summary>
    public class ServiceConfiguration : LifetimeScope
    {
        #region SERIALIZABLE FIELD API

        [Title("Service Installer")]
        [Tooltip("The list of service installers to install into the container.")]
        [SerializeField] private List<ServiceInstallerAsset> assets = new List<ServiceInstallerAsset>();

        #endregion

        /// <summary>
        /// Installs every configured <see cref="ServiceInstallerAsset"/> into the container.
        /// </summary>
        /// <param name="builder">The container builder used to register services.</param>
        protected override void Configure(IContainerBuilder builder)
        {
            Console.LogProgress();

            foreach (var asset in assets)
            {
                asset.Install(builder);
            }

            Console.LogSuccess("<b>All Services</b> are installed completely");
        }
    }
}
