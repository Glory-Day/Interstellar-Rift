using System;
using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class DynamicLocalServiceInstallerBuilder : LocalServiceBuilder<DynamicLocalServiceInstaller>
    {
        private Dictionary<Type, LocalService> _services;

        public DynamicLocalServiceInstallerBuilder()
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _services = null;
        }

        public DynamicLocalServiceInstallerBuilder WithLocalServices(Dictionary<Type, LocalService> services)
        {
            _services = services;

            return this;
        }

        protected override void Validate()
        {
            if (_services == null)
            {
                throw new MissingBuildComponentException(typeof(DynamicLocalServiceInstaller), "Local Service Container");
            }
        }

        protected override DynamicLocalServiceInstaller Create()
        {
            Console.LogProgress();

            return new DynamicLocalServiceInstaller(_services);
        }
    }
}
