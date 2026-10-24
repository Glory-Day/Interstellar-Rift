using Core.Utility;

namespace Core.Object.Module
{
    public abstract class ModuleModelBuilder : Builder<ModuleModel>
    {
        protected readonly ModuleRank Rank;
        protected readonly ModuleData Data;

        protected ModuleModelBuilder(ModuleRank rank, ModuleData data)
        {
            Rank = rank;
            Data = data;
        }
    }
}
