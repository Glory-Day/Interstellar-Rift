using System.Collections.Generic;

namespace Core.Object.Service
{
    public class LocalServiceInstaller : LocalClientBehaviour.Friend
    {
        private readonly IReadOnlyList<LocalClientBehaviour> _clients;
        private readonly ServiceResolver _resolver;

        public LocalServiceInstaller(IReadOnlyList<LocalClientBehaviour> clients,
                                     ServiceResolver resolver)
        {
            _clients = clients;
            _resolver = resolver;
        }

        /// <summary>
        /// Initializes every registered local service by assigning it a <see cref="ServiceResolver"/> and calling <see cref="ClientBehaviour.Install"/> on it.
        /// </summary>
        public void Install()
        {
            var count = _clients.Count;

            for (var i = 0; i < count; i++)
            {
                // Resolver must be assigned before it is initialized, since implementations may use it during initialization.
                Install(_clients[i], _resolver);
            }
        }
    }
}
