using Core.Object.Map;
using GloryDay.Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    /// <summary>
    /// A <see cref="ServiceInstallerAsset"/> that registers the map's camera and zoom controller with the container.
    /// </summary>
    [CreateAssetMenu(fileName = "Map Service Installer Asset",
                     menuName = "Assets/Services/Global/Installer/Map")]
    public class MapServiceInstallerAsset : ServiceInstallerAsset
    {
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            base.Install(builder);

            builder.RegisterComponentInHierarchy<CameraZoomController>();

            Console.LogSuccess("<b>Map Service</b> is installed");
        }
    }
}
