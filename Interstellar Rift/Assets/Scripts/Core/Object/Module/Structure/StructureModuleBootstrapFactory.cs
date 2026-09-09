using System;
using Core.Utility.Extension;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module.Structure
{
    public class StructureModuleBootstrapFactory : IModuleBootstrapFactory
    {
        private readonly ModuleBootstrapConfiguration _configuration;

        public StructureModuleBootstrapFactory(ModuleBootstrapConfiguration configuration)
        {
            Console.LogProgress();

            _configuration = configuration;
        }

        public IBootable Create()
        {
            Console.LogProgress();

            var name = _configuration.Name;

            ModuleBootstrap bootstrap = name switch
            {
                ModuleNames.Structure.CoreModule => new CoreModuleBootstrap(_configuration),
                ModuleNames.Structure.ConnectionModule => new ConnectionModuleBootstrap(_configuration),
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
            };

            Console.LogSuccess($"{nameof(ModuleBootstrap).ToNicifyPascalCase().ToBoldStyle()} for {name} is created.");

            return bootstrap;
        }
    }
}
