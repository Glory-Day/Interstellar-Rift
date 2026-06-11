using Core.Utility.Input;
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
            // Register input action assets.
            builder.RegisterInstance(new MapInputActions());
            builder.RegisterInstance(new ModuleInputActions());

            // Register input actions factories.
            builder.Register<IGameInputActionsFactory, CameraInputActionsFactory>(Lifetime.Scoped);
            builder.Register<IGameInputActionsFactory, DefaultModuleInputActionsFactory>(Lifetime.Scoped);
            builder.Register<IGameInputActionsFactory, BoosterModuleInputActionsFactory>(Lifetime.Scoped);

            // Register input action manager to entry point.
            builder.RegisterEntryPoint<InputActionsManager>();
        }
    }
}
