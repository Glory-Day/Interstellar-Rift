namespace Core.Object.Module.Structure
{
    public sealed class CoreModuleModel : ModuleModel
    {
        public CoreModuleModel(ModuleRank rank, ModuleData data) : base(rank, data) { }

        public void Upgrade(ModuleRank rank, ModuleData data)
        {
            Rank = rank;
        }

        public override string Name => ModuleNames.Structure.CoreModule;
    }
}
