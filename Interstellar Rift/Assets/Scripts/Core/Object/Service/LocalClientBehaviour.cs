using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Service
{
    /// <summary>
    /// Serves as the base class for services that are available only on specific game objects.
    /// </summary>
    public abstract class LocalClientBehaviour : ClientBehaviour
    {
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
            /// Assigns the resolved <see cref="ServiceResolver"/> to a <see cref="LocalClientBehaviour"/> instance.
            /// </summary>
            /// <param name="client">The service instance whose <see cref="Resolver"/> is being set.</param>
            /// <param name="resolver">The resolver to assign.</param>
            protected static void Install(LocalClientBehaviour client, ServiceResolver resolver)
            {
                Console.LogProgress();

                client.Resolver = resolver;
                client.Install();

                Console.LogSuccess($"{nameof(ServiceResolver).ToNicifyPascalCase().ToBoldStyle()} is installed in {client.GetType().Name.ToNicifyPascalCase().ToBoldStyle()}");
            }
        }

        #endregion
    }
}
