using System.Collections.Generic;
using Core.Object.Module;
using Core.Object.Module.Structure;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    public class ServiceResolverFactory : IFactory<ServiceResolver>
    {
        private readonly ModuleBootstrapConfiguration _configuration;

        private List<LocalService> _container;
        private ServiceResolver _resolver;

        public ServiceResolverFactory(ModuleBootstrapConfiguration configuration)
        {
            Console.LogProgress();

            _configuration = configuration;
        }

        public ServiceResolver Create()
        {
            Console.LogProgress();

            _container = new List<LocalService>();
            _resolver = new ServiceResolverBuilder().WithGlobalServices(_configuration.ObjectResolver)
                                                    .WithLocalServiceContainer(_container)
                                                    .Build();

            var factory = _configuration.Specification.services;
            var builder = factory.Create();
            var registry = builder.WithLocalServiceContainer(_container)
                                  .WithServiceResolver(_resolver)
                                  .Build();

            registry.TryRegisterByType<DynamicLocalServiceInstallerBuilderFactoryAsset>(CreateDynamicLocalServiceInstaller);
            registry.TryRegisterByType<ModuleTransformResolverBuilderFactoryAsset>(CreateModuleTransformResolver);
            registry.TryRegisterByType<ModuleModelResolverBuilderFactoryAsset>(CreateModuleModelResolver);

            registry.RegisterAll();

            Console.LogSuccess($"{nameof(ServiceResolver).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return _resolver;
        }

        private LocalService CreateDynamicLocalServiceInstaller(DynamicLocalServiceInstallerBuilderFactoryAsset factory)
        {
            var builder = factory.Create(_resolver) as DynamicLocalServiceInstallerBuilder;
            var service = builder?.WithLocalServices(_container)
                                  .Build();

            return service;
        }

        private LocalService CreateModuleModelResolver(ModuleModelResolverBuilderFactoryAsset factory)
        {
            var name = _configuration.Name;
            var specification = _configuration.Specification;
            var model = new StructureModuleModelFactory(name, specification).Create();

            var builder = factory.Create(_resolver) as ModuleModelResolverBuilder;
            var service = builder?.WithModel(model)
                                 .Build();

            return service;
        }

        private LocalService CreateModuleTransformResolver(ModuleTransformResolverBuilderFactoryAsset factory)
        {
            var clone = _configuration.Clone;
            var transform = clone.transform;

            var builder = factory.Create(_resolver) as ModuleTransformResolverBuilder;
            var service = builder?.WithMainTransform(transform)
                                  .Build();

            return service;
        }
    }
}
