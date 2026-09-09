using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public class ConnectionModuleModelBuilder : ModuleModelBuilder
    {
        public ConnectionModuleModelBuilder(ModuleRank rank, ModuleData data) : base(rank, data)
        {
            Console.LogProgress();
        }

        public override ModuleModel Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ConnectionModuleModel).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ConnectionModuleModel(Rank, Data);
        }
    }
}
