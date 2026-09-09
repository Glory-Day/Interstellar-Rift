namespace Core.Object.Module
{
    public abstract class ModuleModelBuilder : IModuleModelBuildable
    {
        protected readonly ModuleRank Rank;
        protected readonly ModuleData Data;

        protected ModuleModelBuilder(ModuleRank rank, ModuleData data)
        {
            Rank = rank;
            Data = data;
        }

        public abstract ModuleModel Build();
    }
}
