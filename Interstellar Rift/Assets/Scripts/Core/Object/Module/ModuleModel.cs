namespace Core.Object.Module
{
    public abstract class ModuleModel
    {
        protected ModuleModel(ModuleRank rank, ModuleData data)
        {
            Rank = rank;

            Mass = data.Mass;
            Durability = data.Durability;
        }

        public float Mass { get; protected set; }

        public float Durability { get; protected set; }

        public ModuleRank Rank { get; protected set; }

        public abstract string Name { get; }
    }
}
