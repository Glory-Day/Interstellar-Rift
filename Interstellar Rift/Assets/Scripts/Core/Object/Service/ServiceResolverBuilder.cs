using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Exception;
using Core.Utility.Extension;
using GloryDay.Debug;
using VContainer;

namespace Core.Object.Service
{
    public class ServiceResolverBuilder : IBuildable<ServiceResolver>
    {
        private IObjectResolver _resolver;
        private List<LocalService> _container;

        public ServiceResolverBuilder WithGlobalServices(IObjectResolver resolver)
        {
            _resolver = resolver;

            return this;
        }

        public ServiceResolverBuilder WithLocalServiceContainer(List<LocalService> container)
        {
            _container = container;

            return this;
        }

        public ServiceResolver Build()
        {
            Console.LogProgress();

            if (_resolver == null)
            {
                throw new MissingRequiredComponentException(nameof(IObjectResolver));
            }

            if (_container == null)
            {
                throw new MissingRequiredComponentException(nameof(List<LocalService>));
            }

            Console.LogSuccess($"{nameof(ServiceResolver).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ServiceResolver(_resolver, _container);
        }
    }
}
