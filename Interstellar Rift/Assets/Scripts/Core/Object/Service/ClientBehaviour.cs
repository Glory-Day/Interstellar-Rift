using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    public class ClientBehaviour : MonoBehaviour, IServiceable, IInstallable
    {
        /// <inheritdoc/>
        public virtual void Enable()
        {
            Console.LogMessage($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is enabled.");
        }

        /// <inheritdoc/>
        public virtual void Disable()
        {
            Console.LogMessage($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is disabled.");
        }

        public virtual void Install()
        {
            Console.LogSuccess($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed.");
        }
    }
}
