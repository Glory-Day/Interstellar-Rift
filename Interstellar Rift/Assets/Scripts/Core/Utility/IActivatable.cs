namespace Core.Utility
{
    public interface IActivatable
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
