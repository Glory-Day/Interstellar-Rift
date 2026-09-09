using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public sealed class ConnectionModuleModel : ModuleModel
    {
        public ConnectionModuleModel(ModuleRank rank, ModuleData data) : base(rank, data)
        {
            Console.LogProgress();
        }

        public override string Name => ModuleNames.Structure.ConnectionModule;
    }
}
