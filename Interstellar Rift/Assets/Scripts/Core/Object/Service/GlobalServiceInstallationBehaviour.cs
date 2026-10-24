using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// An installer that provides the basic feature for installing global services that depend on <see cref="MonoBehaviour"/>.
    /// </summary>
    public class GlobalServiceInstallationBehaviour : ServiceInstallationBehaviour, IGlobalServiceInstallable
    {
        /// <summary>
        /// Installs global services that depend on <see cref="MonoBehaviour"/>.
        /// </summary>
        /// <param name="resolver">The provider of global services installed via VContainer.</param>
        [Inject]
        public virtual void Install(IObjectResolver resolver)
        {
            Console.LogSuccess($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed.");
        }
    }
}
