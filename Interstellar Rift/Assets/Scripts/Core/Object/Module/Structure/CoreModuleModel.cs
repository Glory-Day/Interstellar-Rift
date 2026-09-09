using GloryDay.Debug;

namespace Core.Object.Module.Structure
{
    public sealed class CoreModuleModel : ModuleModel
    {
        private readonly ModuleDataTable database;

        public CoreModuleModel(ModuleRank rank, ModuleData data, ModuleDataTable database) : base(rank, data)
        {
            Console.LogProgress();
        }

        public void Upgrade(ModuleRank rank)
        {
            Rank = rank;
            Data = database[rank];
        }

        public override string Name => ModuleNames.Structure.CoreModule;
    }
}
