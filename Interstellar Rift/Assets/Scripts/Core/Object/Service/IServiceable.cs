namespace Core.Object.Service
{
    /// <summary>
    /// Represents an object that can be enabled or disabled to control whether it is usable as a service.
    /// </summary>
    public interface IServiceable
    {
        /// <summary>
        /// Enables the service so that it can be used.
        /// </summary>
        public void Enable();

        /// <summary>
        /// Disables the service so that it cannot be used.
        /// </summary>
        public void Disable();
    }
}
