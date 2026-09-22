using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using Core.Utility.Extension;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class LocalServiceRegistry
    {
        private readonly LocalServiceInstallationQueue _queue;
        private readonly List<LocalService> _container;
        private readonly ServiceResolver _resolver;

        public LocalServiceRegistry(LocalServiceInstallationQueue queue,
                                    List<LocalService> container,
                                    ServiceResolver resolver)
        {
            Console.LogProgress();

            _queue = queue;
            _container = container;
            _resolver = resolver;
        }

        public bool TryRegisterByType<T>(Func<T, LocalService> action) where T : LocalServiceFactoryAsset
        {
            Console.LogProgress();

            if (_queue.TryTakeManualInstallationAsset<T>(out var factory) is false)
            {
                return false;
            }

            var service = action.Invoke(factory);
            _container.Add(service);

            Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is registered.");

            return true;
        }

        public void RegisterAllAsAutomatically()
        {
            Console.LogProgress();

            while (_queue.TryTakeAutomaticInstallationAsset(out var factory))
            {
                var service = factory.Create(_resolver);
                _container.Add(service);

                Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is registered.");
            }
        }

        public void RegisterAllAsLazy()
        {
            while (_queue.TryTakeLazyInstallationAsset(out var factory))
            {
                var service = factory.Create(_resolver);
                _container.Add(service);

                Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is registered.");
            }
        }
    }
}
