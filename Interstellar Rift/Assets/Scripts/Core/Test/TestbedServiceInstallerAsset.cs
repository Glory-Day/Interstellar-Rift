#if UNITY_EDITOR

using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Test
{
    [CreateAssetMenu(fileName = "Testbed Service Installer", menuName = "Scriptable Objects/Services/Installer/Testbed")]
    public class TestbedServiceInstallerAsset : ServiceInstallerAsset
    {
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            builder.RegisterComponentInHierarchy<StructureModuleSpawner>();

            Console.LogSuccess("<b>Testbed Service</b> is installed.");
        }
    }
}

#endif
