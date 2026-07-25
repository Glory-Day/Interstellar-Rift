using System;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module.Structure
{
    public class StructureModuleModelFactory : IFactory<ModuleModel>
    {
        private readonly string _name;
        private readonly ModuleRank _rank;
        private readonly ModuleDataTable _database;

        public StructureModuleModelFactory(string name, ModuleRank rank, ModuleDataTable database)
        {
            _name = name;
            _rank = rank;
            _database = database;
        }

        public ModuleModel Create()
        {
            Console.LogProgress();

            var data = _database[_rank];

            return _name switch
            {
                ModuleNames.Structure.CoreModule => new CoreModuleModelBuilder(_rank, data)
                                                   .WithDatabase(_database)
                                                   .Build(),
                ModuleNames.Structure.ConnectionModule => new ConnectionModuleModelBuilder(_rank, data).Build(),
                _ => throw new ArgumentOutOfRangeException(nameof(_name), _name, null)
            };
        }
    }
}
