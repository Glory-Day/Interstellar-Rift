namespace Core.Object.Module.Structure
{
    public sealed class ConnectionModuleModel : ModuleModel
    {
        public ConnectionModuleModel(ModuleRank rank, ModuleData data) : base(rank, data) { }

        public override string Name => ModuleNames.Structure.ConnectionModule;
    }
}
