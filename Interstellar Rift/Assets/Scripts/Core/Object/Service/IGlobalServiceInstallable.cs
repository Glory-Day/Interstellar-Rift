using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// Defines a method for installing global services.
    /// It defines the installation of global services that depend on other APIs.
    /// </summary>
    public interface IGlobalServiceInstallable
    {
        /// <summary>
        /// Installs global services that depend on other APIs.
        /// </summary>
        /// <param name="resolver">The provider of global services installed via VContainer.</param>
        public void Install(IObjectResolver resolver);
    }
}
