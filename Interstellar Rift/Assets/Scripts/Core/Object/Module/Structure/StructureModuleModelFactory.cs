using System;
using Core.Test;
using Core.Utility.Extension;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module.Structure
{
    public class StructureModuleModelFactory : IModuleModelFactory
    {
        private readonly string _name;

        private readonly ModuleRank _rank;
        private readonly ModuleDataTable _database;

        public StructureModuleModelFactory(string name, ModuleSpawnSpecification specification)
        {
            Console.LogProgress();

            _name = name;
            _rank = specification.rank;
            _database = specification.database;
        }

        public ModuleModel Create()
        {
            Console.LogProgress();

            var data = _database[_rank];
            var model = _name switch
            {
                ModuleNames.Structure.CoreModule => new CoreModuleModelBuilder(_rank, data)
                                                   .WithDatabase(_database)
                                                   .Build(),
                ModuleNames.Structure.ConnectionModule => new ConnectionModuleModelBuilder(_rank, data).Build(),
                _ => throw new ArgumentOutOfRangeException(nameof(_name), _name, null)
            };

            Console.LogSuccess($"{nameof(ModuleModel).ToNicifyPascalCase().ToBoldStyle()} for {_name} is created.");

            return model;
        }
    }
}
