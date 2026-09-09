using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    public class DynamicLocalServiceInstallerBuilder : LocalService, IBuildable<DynamicLocalServiceInstaller>
    {
        private List<LocalService> _services;

        public DynamicLocalServiceInstallerBuilder(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _services = null;
        }

        public DynamicLocalServiceInstallerBuilder WithLocalServices(List<LocalService> services)
        {
            _services = services;

            return this;
        }

        public DynamicLocalServiceInstaller Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(DynamicLocalServiceInstaller).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new DynamicLocalServiceInstaller(_services, Resolver);
        }
    }
}
