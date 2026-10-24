using System.Collections.Generic;
using UnityEngine;

namespace Core.Object.Service
{
    /// <summary>
    /// An installer that lazily installs local services that depend on <see cref="MonoBehaviour"/>.
    /// </summary>
    public class UnityDependentLocalServiceInstaller : LocalServiceInstallationBehaviour.Friend
    {
        private readonly IReadOnlyList<LocalServiceInstallationBehaviour> _installers;
        private readonly ServiceResolver _resolver;

        /// <param name="installers">The list of installers for local services that depend on <see cref="MonoBehaviour"/> and must be installed.</param>
        /// <param name="resolver">The resolver used to look up the services that the installed local services depend on.</param>
        public UnityDependentLocalServiceInstaller(IReadOnlyList<LocalServiceInstallationBehaviour> installers,
                                         ServiceResolver resolver)
        {
            _installers = installers;
            _resolver = resolver;
        }

        /// <summary>
        /// Lazily installs local services that depend on <see cref="MonoBehaviour"/>.
        /// </summary>
        public void Install()
        {
            var count = _installers.Count;

            for (var i = 0; i < count; i++)
            {
                // The resolver requires manual and automatic installation to have been completed before lazy installation.
                Install(_installers[i], _resolver);
            }
        }
    }
}
