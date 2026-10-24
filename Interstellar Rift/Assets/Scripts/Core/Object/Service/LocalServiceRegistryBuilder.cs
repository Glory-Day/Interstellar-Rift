using System;
using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class LocalServiceRegistryBuilder : Builder<LocalServiceRegistry>
    {
        private readonly LocalServiceInstallationQueue _queue;

        private Dictionary<Type, LocalService> _container;
        private ServiceResolver _resolver;

        public LocalServiceRegistryBuilder(List<LocalServiceFactoryAsset> assets)
        {
            Console.LogProgress();

            _queue = new LocalServiceInstallationQueue(assets);
        }

        public LocalServiceRegistryBuilder WithLocalServiceContainer(Dictionary<Type, LocalService> container)
        {
            _container = container;

            return this;
        }

        public LocalServiceRegistryBuilder WithServiceResolver(ServiceResolver resolver)
        {
            _resolver = resolver;

            return this;
        }

        protected override void Validate()
        {
            if (_container == null)
            {
                throw new MissingBuildComponentException(typeof(LocalServiceRegistry), "Local Service Container");
            }

            if (_resolver == null)
            {
                throw new MissingBuildComponentException(typeof(LocalServiceRegistry), "Service Resolver");
            }
        }

        protected override LocalServiceRegistry Create()
        {
            Console.LogProgress();

            return new LocalServiceRegistry(_queue, _container, _resolver);
        }
    }
}
