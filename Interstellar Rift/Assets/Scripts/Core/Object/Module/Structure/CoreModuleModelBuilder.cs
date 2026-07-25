using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public class CoreModuleModelBuilder : ModuleModelBuilder
    {
        private ModuleDataTable _database;

        public CoreModuleModelBuilder(ModuleRank rank, ModuleData data) : base(rank, data) { }

        public CoreModuleModelBuilder WithDatabase(ModuleDataTable database)
        {
            _database = database;

            return this;
        }

        public override ModuleModel Build()
        {
            Console.LogProgress();

            return new CoreModuleModel(Rank, Data, _database);
        }
    }
}
