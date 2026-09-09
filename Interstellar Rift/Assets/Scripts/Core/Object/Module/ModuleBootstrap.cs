using Core.Object.Service;
using Core.Utility.Exception;
using Core.Utility.Extension;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleBootstrap : IBootable
    {
        protected readonly ModuleBootstrapConfiguration Configuration;

        protected ModuleBootstrap(ModuleBootstrapConfiguration configuration)
        {
            Console.LogProgress();

            Configuration = configuration;
        }

        public virtual void Boot()
        {
            Console.LogProgress();

            try
            {
                var resolver = new ServiceResolverFactory(Configuration).Create();

                var clone = Configuration.Clone;
                var initializer = clone.GetComponentInChildren<LocalClientInitializer>();
                initializer.Initialize(resolver);

                var drawer = resolver.GetLocalService<ModuleTextureDrawer>();
                drawer.Draw();

                Console.LogSuccess("Module".ToBoldStyle() + "is booting completed.");
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
