namespace Core.Object.Module
{
    public abstract class ModuleBootstrapBuilder<TModuleBoostrap, TBuilder> : IModuleBootstrapBuilder<TModuleBoostrap, TBuilder>
    {
        protected ModuleRank Rank;
        protected ModuleModel Model;

        protected ModuleServiceResolver Resolver;

        protected UpdateEventHandler UpdateEventHandler;

        protected ModuleBootstrapBuilder(ModuleRank rank, ModuleModel model)
        {
            Rank = rank;
            Model = model;
        }

        public TBuilder WithServiceMediator(ModuleServiceResolver resolver)
        {
            Resolver = resolver;

            return (TBuilder)(object)this;
        }

        public TBuilder WithUpdateEventHandler(UpdateEventHandler updateEventHandler)
        {
            UpdateEventHandler = updateEventHandler;

            return (TBuilder)(object)this;
        }

        public abstract TModuleBoostrap Build();
    }
}
