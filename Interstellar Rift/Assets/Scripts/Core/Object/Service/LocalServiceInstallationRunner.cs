using System.Collections.Generic;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that collects the <see cref="LocalServiceInstallationBehaviour"/> components
    /// in its hierarchy and runs their installation.
    /// </summary>
    public class LocalServiceInstallationRunner : MonoBehaviour
    {
        private UnityDependentLocalServiceInstaller _installer;

        private readonly Dictionary<InstallationMode, List<LocalServiceInstallationBehaviour>> _installers =
            new Dictionary<InstallationMode, List<LocalServiceInstallationBehaviour>>()
            {
                { InstallationMode.Manual, new List<LocalServiceInstallationBehaviour>() },
                { InstallationMode.Automatic, new List<LocalServiceInstallationBehaviour>() },
                { InstallationMode.Lazy, new List<LocalServiceInstallationBehaviour>() },
            };

        public void Import()
        {
            Console.LogProgress();

            var installers = GetComponentsInChildren<LocalServiceInstallationBehaviour>(true);
            foreach (var installer in installers)
            {
                _installers[installer.Mode].Add(installer);
            }
        }

        /// <summary>
        /// Collects every <see cref="LocalServiceInstallationBehaviour"/> on this object and its children, including inactive ones,
        /// and installs them with the specified resolver.
        /// </summary>
        /// <param name="resolver">The resolver used to look up the services that the installed local services depend on.</param>
        /// <remarks>
        /// Manual and automatic installation must be completed before this method is called.
        /// The installers run in the order in which they are found in the hierarchy.
        /// </remarks>
        public void Install(ServiceResolver resolver, InstallationMode mode)
        {
            Console.LogProgress();

            _installer = new UnityDependentLocalServiceInstaller(_installers[mode], resolver);
            _installer.Install();

            Console.LogSuccess("All local services that depend on the Unity API have been installed.");

            _installers[mode].Clear();
        }
    }
}
