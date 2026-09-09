using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public class ConnectionModuleBootstrap : ModuleBootstrap
    {
        public ConnectionModuleBootstrap(ModuleBootstrapConfiguration configuration) : base(configuration)
        {
            Console.LogProgress();
        }
    }
}
