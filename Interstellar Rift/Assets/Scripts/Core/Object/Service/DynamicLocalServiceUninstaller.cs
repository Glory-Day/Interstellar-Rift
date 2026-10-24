using System;
using System.Collections.Generic;
using Core.Utility.Extensions;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class DynamicLocalServiceUninstaller : LocalService
    {
        private Dictionary<Type, LocalService> _container;

        public DynamicLocalServiceUninstaller(Dictionary<Type, LocalService> container)
        {
            Console.LogProgress();

            _container = container;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _container = null;
        }

        public void Uninstall<TLocalService>() where TLocalService : LocalService
        {
            Console.LogProgress();

            var service = _container[typeof(TLocalService)];
            service.Dispose();

            _container.Remove(typeof(TLocalService));

            Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is uninstalled dynamically.");
        }
    }
}
