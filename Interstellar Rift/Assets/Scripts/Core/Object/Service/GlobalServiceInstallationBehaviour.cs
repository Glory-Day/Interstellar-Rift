using Core.Utility.Extension;
using GloryDay.Debug;
using VContainer;

namespace Core.Object.Service
{
    public class GlobalServiceInstallationBehaviour : ServiceInstallationBehaviour, IGlobalServiceInstallable
    {
        [Inject]
        public virtual void Install(IObjectResolver resolver)
        {
            Console.LogSuccess($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed.");
        }
    }
}
