using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    public class LocalServiceRegistryBuilder : IBuildable<LocalServiceRegistry>
    {
        private readonly LocalServiceInstallationQueue _queue;

        private List<LocalService> _container;
        private ServiceResolver _resolver;

        public LocalServiceRegistryBuilder(List<LocalServiceFactoryAsset> assets)
        {
            Console.LogProgress();

            _queue = new LocalServiceInstallationQueue(assets);
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

            return new LocalServiceRegistry(_queue, _container, _resolver);
        }
    }
}
