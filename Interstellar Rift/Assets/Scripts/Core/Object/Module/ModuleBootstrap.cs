using Core.Object.Service;
using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleBootstrap : IBootable
    {
        private readonly ModuleBootstrapConfiguration _configuration;

        protected ModuleBootstrap(ModuleBootstrapConfiguration configuration)
        {
            Console.LogProgress();

            _configuration = configuration;
        }

        public virtual void Boot()
        {
            Console.LogProgress();

            try
            {
                var clone = _configuration.Clone;
                var initializer = clone.GetComponentInChildren<LocalServiceInstallationRunner>();
                var resolver = new ServiceResolverFactory(initializer, _configuration).Create();

                var registry = resolver.GetGlobalService<LocalServiceRepository>();
                var collider2Ds = clone.GetComponentsInChildren<Collider2D>();
                foreach (var collider2D in collider2Ds)
                {
                    registry.Register(collider2D, resolver);
                }

                var drawer = resolver.GetLocalService<ModuleTextureDrawer>();
                drawer.Draw();

                Console.LogSuccess("Module".ToBoldStyle() + " is booting completed.");
            }
            catch (MissingRequiredGlobalServiceException exception)
            {
                Console.LogError(exception.Message);
            }
            catch (MissingRequiredLocalServiceException exception)
            {
                Console.LogError(exception.Message);
            }
        }
    }
}
