using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// Initializes all local services registered with a <see cref="ServiceResolver"/> for a single game object.
    /// </summary>
    public class LocalClientInitializer : MonoBehaviour
    {
        private LocalServiceInstaller _installer;

        public void Initialize(ServiceResolver resolver)
        {
            Console.LogProgress();

            var clients = GetComponentsInChildren<LocalClientBehaviour>();

            _installer = new LocalServiceInstaller(clients, resolver);
            _installer.Install();

            Console.LogSuccess("All clients have been initialized.");
        }
    }
}
