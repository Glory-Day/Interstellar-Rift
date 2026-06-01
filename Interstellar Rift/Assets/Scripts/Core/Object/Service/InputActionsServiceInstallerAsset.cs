using Core.Utility.Input;
using GloryDay.Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    [CreateAssetMenu(fileName = "Input Actions Service Installer", menuName = "Scriptable Objects/Services/Installer/Input Actions")]
    public class InputActionsServiceInstallerAsset : ServiceInstallerAsset
    {
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            // Register input action assets.
            builder.RegisterInstance(new MapInputActions());
            builder.RegisterInstance(new ModuleInputActions());

            // Register input actions factories.
            builder.Register<IGameInputActionsFactory, CameraInputActionsFactory>(Lifetime.Scoped);
            builder.Register<IGameInputActionsFactory, BoosterInputActionsFactory>(Lifetime.Scoped);

            // Register input action manager to entry point.
            builder.RegisterEntryPoint<InputActionsManager>();

            Console.LogSuccess("<b>Input Actions Service</b> is installed");
        }
    }
}
