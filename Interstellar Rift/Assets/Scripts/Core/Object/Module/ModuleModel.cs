namespace Core.Object.Module
{
    public abstract class ModuleModel
    {
        protected ModuleModel(ModuleRank rank, ModuleData data)
        {
            Rank = rank;
            Data = data;
        }

        public ModuleRank Rank { get; protected set; }

        public ModuleData Data { get; protected set; }

        public abstract string Name { get; }
    }
}
