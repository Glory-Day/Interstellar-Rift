using GloryDay.Debug;
using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// A <see cref="ServiceInstallerAsset"/> reserved for registering module-related services with the container.
    /// </summary>
    [CreateAssetMenu(fileName = "Module Service Installer", menuName = "Scriptable Objects/Services/Installer/Module")]
    public class ModuleServiceInstallerAsset : ServiceInstallerAsset
    {
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            Console.LogSuccess("<b>Module Service</b> is installed.");
        }
    }
}
