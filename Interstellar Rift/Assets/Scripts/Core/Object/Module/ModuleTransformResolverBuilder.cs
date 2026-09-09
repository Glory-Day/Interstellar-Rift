using Core.Object.Service;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTransformResolverBuilder : LocalService, IBuildable<ModuleTransformResolver>
    {
        private Transform _mainTransform;

        public ModuleTransformResolverBuilder(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _mainTransform = null;
        }

        public ModuleTransformResolverBuilder WithMainTransform(Transform transform)
        {
            _mainTransform = transform;

            return this;
        }

        public ModuleTransformResolver Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleTransformResolver).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ModuleTransformResolver(_mainTransform, Resolver);
        }
    }
}
