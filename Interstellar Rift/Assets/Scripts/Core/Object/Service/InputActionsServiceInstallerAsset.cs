using Core.Utility.Input;
using GloryDay.Debug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Object.Service
{
    /// <summary>
    /// A <see cref="ServiceInstallerAsset"/> that registers the project's input action assets, factories, and entry point with the container.
    /// </summary>
    [CreateAssetMenu(fileName = "Input Actions Service Installer", menuName = "Scriptable Objects/Services/Installer/Input Actions")]
    public class InputActionsServiceInstallerAsset : ServiceInstallerAsset
    {
        /// <inheritdoc/>
        public override void Install(IContainerBuilder builder)
        {
            Console.LogProgress();

            // Register input action assets.
            builder.RegisterInstance(new MapInputActions());
            builder.RegisterInstance(new ModuleInputActions());

            // Register input actions factories.
            builder.Register<IGameInputActionsFactory, CameraInputActionsFactory>(Lifetime.Scoped);
            builder.Register<IGameInputActionsFactory, DefaultModuleInputActionsFactory>(Lifetime.Scoped);
            builder.Register<IGameInputActionsFactory, BoosterModuleInputActionsFactory>(Lifetime.Scoped);

            // Register input action manager to entry point.
            builder.RegisterEntryPoint<InputActionsManager>();

            Console.LogSuccess("<b>Input Actions Service</b> is installed");
        }
    }
}
