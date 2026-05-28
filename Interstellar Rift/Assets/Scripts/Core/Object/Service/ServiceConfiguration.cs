using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    public class ServiceConfiguration : LifetimeScope
    {
        #region SERIALIZABLE FIELD API

        [Title("Service Installer")]
        [SerializeField] private List<ServiceInstallerAsset> assets = new List<ServiceInstallerAsset>();

        #endregion

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var asset in assets)
            {
                asset.Install(builder);
            }
        }
    }
}
