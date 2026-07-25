using Core.Utility.Pool;
using GloryDay.Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    /// <summary>
    /// A <see cref="ServiceInstallerAsset"/> that registers the project's global services with the container.
    /// </summary>
    [CreateAssetMenu(fileName = "Global Service Installer", menuName = "Scriptable Objects/Services/Installer/Global")]
    public class GlobalServiceInstallerAsset : ServiceInstallerAsset
    {
        /// <inheritdoc/>
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            builder.RegisterInstance(new ObjectPool());

            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<CoroutineRunner>();
            builder.RegisterComponentInHierarchy<UpdateEventHandler>();

            Console.LogSuccess("<b>Global Service</b> is installed");
        }
    }
}
