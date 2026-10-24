using Core.Object.Service;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine.SocialPlatforms;

namespace Core.Object.Module
{
    public class ModuleModelResolverBuilder : LocalServiceBuilder<ModuleModelResolver>
    {
        private ModuleModel _model;

        public ModuleModelResolverBuilder()
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _model = null;
        }

        public ModuleModelResolverBuilder WithModel(ModuleModel model)
        {
            _model = model;

            return this;
        }

        protected override void Validate()
        {
            if (_model == null)
            {
                throw new MissingBuildComponentException(typeof(ModuleModelResolver), "Module Model");
            }
        }

        protected override ModuleModelResolver Create()
        {
            Console.LogProgress();

            return new ModuleModelResolver(_model);
        }
    }
}
