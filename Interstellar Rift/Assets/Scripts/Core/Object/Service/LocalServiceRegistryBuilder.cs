using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    public class LocalServiceRegistryBuilder : IBuildable<LocalServiceRegistry>
    {
        private readonly List<LocalServiceFactoryAsset> _assets;

        private List<LocalService> _container;
        private ServiceResolver _resolver;

        public LocalServiceRegistryBuilder(List<LocalServiceFactoryAsset> assets)
        {
            Console.LogProgress();

            _assets = assets;
        }

        public LocalServiceRegistryBuilder WithLocalServiceContainer(List<LocalService> container)
        {
            _container = container;

            return this;
        }

        public LocalServiceRegistryBuilder WithServiceResolver(ServiceResolver resolver)
        {
            _resolver = resolver;

            return this;
        }

        public LocalServiceRegistry Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(LocalServiceRegistry).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new LocalServiceRegistry(_assets, _container, _resolver);
        }
    }
}
