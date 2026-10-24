namespace Core.Utility
{
    /// <summary>
    /// Defines methods for enabling and disabling the functionality of the implementing object.
    /// </summary>
    /// <remarks>
    /// The implementing object is enabled when it is created. While disabled, it does not perform
    /// its functionality. Unlike disposal, disabling is reversible: it does not release the resources
    /// held by the object, and the object can be enabled again after being disabled.
    /// </remarks>
    public interface IEnableable
    {
        /// <summary>
        /// Enables the functionality of this object so that it can be used.
        /// </summary>
        public void Enable();

        /// <summary>
        /// Disables the functionality of this object so that it cannot be used
        /// until <see cref="Enable"/> is called again.
        /// </summary>
        public void Disable();

        /// <summary>
        /// Gets a value indicating whether the functionality of this service is enabled.
        /// </summary>
        public bool IsEnabled { get; }
    }
}
