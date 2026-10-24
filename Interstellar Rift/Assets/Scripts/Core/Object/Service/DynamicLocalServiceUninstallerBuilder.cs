using System;
using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Exceptions;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    public class DynamicLocalServiceUninstallerBuilder : LocalServiceBuilder<DynamicLocalServiceUninstaller>
    {
        private Dictionary<Type, LocalService> _container;

        public DynamicLocalServiceUninstallerBuilder()
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _container = null;
        }

        public DynamicLocalServiceUninstallerBuilder WithLocalServiceContainer(Dictionary<Type, LocalService> container)
        {
            _container = container;

            return this;
        }

        protected override void Validate()
        {
            if (_container == null)
            {
                throw new MissingBuildComponentException(typeof(DynamicLocalServiceInstaller), "Local Service Container");
            }
        }

        protected override DynamicLocalServiceUninstaller Create()
        {
            Console.LogProgress();

            return new DynamicLocalServiceUninstaller(_container);
        }
    }
}
