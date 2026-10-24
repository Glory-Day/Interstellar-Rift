using Core.Utility.Extensions;
using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public class ConnectionModuleModelBuilder : ModuleModelBuilder
    {
        public ConnectionModuleModelBuilder(ModuleRank rank, ModuleData data) : base(rank, data)
        {
            Console.LogProgress();
        }

        protected override void Validate() { }

        protected override ModuleModel Create()
        {
            Console.LogProgress();

            return new ConnectionModuleModel(Rank, Data);
        }
    }
}
