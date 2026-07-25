using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public class ConnectionModuleModelBuilder : ModuleModelBuilder
    {
        public ConnectionModuleModelBuilder(ModuleRank rank, ModuleData data) : base(rank, data) { }

        public override ModuleModel Build()
        {
            Console.LogProgress();

            return new ConnectionModuleModel(Rank, Data);
        }
    }
}
