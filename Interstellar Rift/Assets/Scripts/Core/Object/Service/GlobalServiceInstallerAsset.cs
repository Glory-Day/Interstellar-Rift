using Core.Utility.Pool;
using GloryDay.Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    [CreateAssetMenu(fileName = "Global Service Installer", menuName = "Scriptable Objects/Services/Installer/Global")]
    public class GlobalServiceInstallerAsset : ServiceInstallerAsset
    {
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            builder.RegisterInstance(new ObjectPool());

            builder.RegisterComponentInHierarchy<CoroutineRunner>();
            builder.RegisterComponentInHierarchy<UpdateEventHandler>();

            Console.LogSuccess("<b>Global Service</b> is installed");
        }
    }
}
