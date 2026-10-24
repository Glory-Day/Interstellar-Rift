namespace Core.Object.Service
{
    /// <summary>
    /// Defines a method for installing local services that depend on external APIs, such as the Unity API.
    /// </summary>
    public interface ILocalServiceInstallable
    {
        /// <summary>
        /// Installs local services that depend on external APIs.
        /// </summary>
        public void Install();

        public InstallationMode Mode { get; }
    }
}
