using System;
using System.Collections.Generic;
using Core.Utility.Attributes;
using Core.Utility.Extensions;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class DynamicLocalServiceInstaller : LocalService
    {
        private Dictionary<Type, LocalService> _services;

        public DynamicLocalServiceInstaller(Dictionary<Type, LocalService> services)
        {
            Console.LogProgress();

            _services = services;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _services = null;
        }

        public void Install(LocalService service)
        {
            Console.LogProgress();

            foreach (var type in service.GetType().GetHierarchyWithoutExcluded<ExcludeFromInstallationAttribute>())
            {
                _services.Add(type, service);
            }

            Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed dynamically.");
        }
    }
}
