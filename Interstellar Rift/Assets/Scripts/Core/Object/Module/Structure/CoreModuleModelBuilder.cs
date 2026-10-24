using Core.Utility.Exceptions;
using Core.Utility.Extensions;
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

        protected override void Validate()
        {
            if (_database == null)
            {
                throw new MissingBuildComponentException(typeof(CoreModuleModel), "Module Data Table");
            }
        }

        protected override ModuleModel Create()
        {
            Console.LogProgress();

            return new CoreModuleModel(Rank, Data, _database);
        }
    }
}
