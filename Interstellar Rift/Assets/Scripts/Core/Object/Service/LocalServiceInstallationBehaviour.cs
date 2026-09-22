using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    /// <summary>
    /// Serves as the base class for services that are available only on specific game objects.
    /// </summary>
    public abstract class LocalServiceInstallationBehaviour : ServiceInstallationBehaviour, ILocalServiceInstallable
    {
        public virtual void Install()
        {
            Console.LogSuccess($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} is installed.");
        }

        /// <summary>
        /// The <see cref="ServiceResolver"/> used to resolve local and global services for this instance.
        /// </summary>
        protected ServiceResolver Resolver { get; private set; }

        #region FRIEND CLASS API

        /// <summary>
        /// Restricts write access to <see cref="Resolver"/> to trusted initializer classes that inherit from this type.
        /// </summary>
        public abstract class Friend
        {
            /// <summary>
            /// Assigns the resolved <see cref="ServiceResolver"/> to a <see cref="LocalServiceInstallationBehaviour"/> instance.
            /// </summary>
            /// <param name="installer">The service instance whose <see cref="Resolver"/> is being set.</param>
            /// <param name="resolver">The resolver to assign.</param>
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
