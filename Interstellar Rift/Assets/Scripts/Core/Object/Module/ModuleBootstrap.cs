using Core.Object.Service;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleBootstrap : IBootable
    {
        private readonly ModuleModel _model;

        private readonly ServiceResolver _resolver;

        protected ModuleBootstrap(ModuleModel model, ServiceResolver resolver)
        {
            _model = model;
            _resolver = resolver;
        }

        public virtual void Boot()
        {
            Console.LogProgress();

            var rank = _model.Rank;

            IInitializable initializer = new ModuleLocalServiceInitializer(_resolver);
            initializer.Initialize();

            var updateEventHandler = _resolver.GetGlobalService<UpdateEventHandler>();
            var factory = new ColorApplicatorFactory(rank, updateEventHandler);
            var applicator = factory.Create();

            var renderer = _resolver.GetLocalService<ModuleTextureRenderer>();
            renderer.Applicator = applicator;
            renderer.Draw();
        }
    }
}
