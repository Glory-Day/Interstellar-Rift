using Core.Utility.Extensions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// The base class for installers of local services that depend on <see cref="MonoBehaviour"/>.
    /// </summary>
    public abstract class LocalServiceInstallationBehaviour : ServiceInstallationBehaviour, ILocalServiceInstallable
    {
        /// <summary>
        /// Installs local services that depend on <see cref="MonoBehaviour"/>.
        /// </summary>
        public virtual void Install()
        {
            Console.LogSuccess($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed.");
        }

        /// <summary>
        /// Gets the resolver used to look up the services that the installed local services depend on.
        /// </summary>
        /// <remarks>
        /// The resolver is assigned by <see cref="Friend.Install"/> before <see cref="Install"/> is invoked.
        /// </remarks>
        protected ServiceResolver Resolver { get; private set; }

        public abstract InstallationMode Mode { get; }

        #region FRIEND CLASS API

        /// <summary>
        /// Grants classes that inherit this type the authority to install a <see cref="LocalServiceInstallationBehaviour"/>.
        /// </summary>
        public abstract class Friend
        {
            /// <summary>
            /// Assigns the resolver to the specified installer and runs its installation.
            /// </summary>
            /// <param name="installer">The installer to run.</param>
            /// <param name="resolver">The resolver used to look up the services that the installed local services depend on.</param>
            protected static void Install(LocalServiceInstallationBehaviour installer, ServiceResolver resolver)
            {
                Console.LogProgress();

                installer.Resolver = resolver;
                installer.Install();

                Console.LogSuccess($"{nameof(ServiceResolver).ToNicifyPascalCase().ToBoldStyle()} is installed in {installer.GetType().Name.ToNicifyPascalCase().ToBoldStyle()}");
            }
        }

        #endregion
    }
}
