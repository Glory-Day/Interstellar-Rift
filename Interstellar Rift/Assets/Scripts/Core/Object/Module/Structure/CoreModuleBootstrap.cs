using Core.Object.Service;
using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public class CoreModuleBootstrap : ModuleBootstrap
    {
        public CoreModuleBootstrap(ModuleBootstrapConfiguration configuration) : base(configuration)
        {
            Console.LogProgress();
        }
    }
}
