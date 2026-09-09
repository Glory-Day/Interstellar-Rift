using Core.Object.Service;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Module
{
    public class ModuleModelResolverBuilder : LocalService, IBuildable<ModuleModelResolver>
    {
        private ModuleModel _model;

        public ModuleModelResolverBuilder(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _model = null;
        }

        public ModuleModelResolverBuilder WithModel(ModuleModel model)
        {
            _model = model;

            return this;
        }

        public ModuleModelResolver Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleModelResolver).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ModuleModelResolver(_model, Resolver);
        }
    }
}
