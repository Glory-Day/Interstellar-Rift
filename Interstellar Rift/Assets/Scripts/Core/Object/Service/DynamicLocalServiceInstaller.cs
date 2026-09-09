using System.Collections.Generic;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    public class DynamicLocalServiceInstaller : LocalService
    {
        private List<LocalService> _services;

        public DynamicLocalServiceInstaller(List<LocalService> services, ServiceResolver resolver) : base(resolver)
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

            _services.Add(service);

            Console.LogSuccess($"{service.GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed dynamically.");
        }
    }
}
