using Core.Object.Service;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTransformResolverBuilder : LocalServiceBuilder<ModuleTransformResolver>
    {
        private Transform _mainTransform;

        public ModuleTransformResolverBuilder()
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _mainTransform = null;
        }

        public ModuleTransformResolverBuilder WithMainTransform(Transform transform)
        {
            _mainTransform = transform;

            return this;
        }

        protected override void Validate()
        {
            if (_mainTransform == null)
            {
                throw new MissingBuildComponentException(typeof(ModuleTransformResolver), "Main Transform");
            }
        }

        protected override ModuleTransformResolver Create()
        {
            Console.LogProgress();

            return new ModuleTransformResolver(_mainTransform);
        }
    }
}
