using Core.Utility.Input;
using VContainer;
using VContainer.Unity;

namespace Core.Object
{
    public class InputActionServiceConfiguration : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Register input action assets.
            builder.RegisterInstance(new MapInputActions());

            // Register input actions factories.
            builder.Register<IGameInputActionsFactory, CameraInputActionsFactory>(Lifetime.Singleton);

            // Register input action manager to entry point.
            builder.RegisterEntryPoint<InputActionManager>();
        }
    }
}
