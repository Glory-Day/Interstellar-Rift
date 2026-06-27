using System;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module.Structure
{
    public class StructureModuleBootstrapFactory : IFactory<IBootable>
    {
        private readonly ModuleModel _model;
        private readonly ModuleServiceResolver _resolver;

        public StructureModuleBootstrapFactory(ModuleModel model,  ModuleServiceResolver resolver)
        {
            _model = model;
            _resolver = resolver;
        }

        public IBootable Create()
        {
            Console.LogProgress();

            var name = _model.Name;

            return name switch
            {
                ModuleNames.Structure.CoreModule => new CoreModuleBootstrap(_model, _resolver),
                ModuleNames.Structure.ConnectionModule => new ConnectionModuleBootstrap(_model,  _resolver),
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
            };
        }
    }
}
