using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// Initializes all local services registered with a <see cref="ServiceResolver"/> for a single game object.
    /// </summary>
    public class LocalServiceInstallationRunner : MonoBehaviour
    {
        private LocalServiceInstaller _installer;

        public void Install(ServiceResolver resolver)
        {
            Console.LogProgress();

            var installers = GetComponentsInChildren<LocalServiceInstallationBehaviour>(true);

            _installer = new LocalServiceInstaller(installers, resolver);
            _installer.Install();

            Console.LogSuccess("All clients have been initialized.");
        }
    }
}
