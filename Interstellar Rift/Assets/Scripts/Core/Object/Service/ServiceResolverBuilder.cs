using System;
using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class ServiceResolverBuilder : Builder<ServiceResolver>
    {
        private IObjectResolver _resolver;
        private Dictionary<Type, LocalService> _container;

        public ServiceResolverBuilder WithGlobalServices(IObjectResolver resolver)
        {
            _resolver = resolver;

            return this;
        }

        public ServiceResolverBuilder WithLocalServiceContainer(Dictionary<Type, LocalService> container)
        {
            _container = container;

            return this;
        }

        protected override void Validate()
        {
            if (_resolver == null)
            {
                throw new MissingBuildComponentException(typeof(ServiceResolver), "Global Services");
            }

            if (_container == null)
            {
                throw new MissingBuildComponentException(typeof(ServiceResolver), "Local Service Container");
            }
        }

        protected override ServiceResolver Create()
        {
            Console.LogProgress();

            return new ServiceResolver(_resolver, _container);
        }
    }
}
