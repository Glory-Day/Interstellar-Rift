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
    [CreateAssetMenu(fileName = "Module Service Installer Asset",
                     menuName = "Assets/Services/Global/Installer/Module")]
    public class ModuleServiceInstallerAsset : ServiceInstallerAsset
    {
        /// <inheritdoc/>
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            base.Install(builder);

            builder.RegisterInstance(new ObjectPool());

            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<UpdateEventHandler>();

            Console.LogSuccess("<b>Global Service</b> is installed");
        }
    }
}
