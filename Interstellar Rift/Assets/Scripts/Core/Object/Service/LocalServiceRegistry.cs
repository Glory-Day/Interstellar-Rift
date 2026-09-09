using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility.Extension;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class LocalServiceRegistry
    {
        private readonly List<LocalServiceFactoryAsset> _assets;
        private readonly List<LocalService> _container;
        private readonly ServiceResolver _resolver;

        public LocalServiceRegistry(List<LocalServiceFactoryAsset> assets,
                                    List<LocalService> container,
                                    ServiceResolver resolver)
        {
            Console.LogProgress();

            _assets = assets;
            _container = container;
            _resolver = resolver;
        }

        public bool TryRegisterByType<T>(Func<T, LocalService> action) where T : LocalServiceFactoryAsset
        {
            Console.LogProgress();

            var factory = _assets.OfType<T>().FirstOrDefault();
            if (factory is null)
            {
                return false;
            }

            var service = action.Invoke(factory);
            _container.Add(service);

            _assets.Remove(factory);

            Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is registered.");

            return true;
        }

        public void RegisterAll()
        {
            var count = _assets.Count;
            for (var i = 0; i < count; i++)
            {
                var factory = _assets[i];
                var service = factory.Create(_resolver);

                Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is registered.");

                _container.Add(service);
            }

            _assets.Clear();
        }
    }
}
