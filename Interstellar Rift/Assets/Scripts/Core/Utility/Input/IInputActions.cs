namespace Core.Utility.Input
{
    public interface IInputActions
    {
        /// <summary>
        /// Enable the input actions.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disable the input actions.
        /// </summary>
        void Disable();

        /// <returns>
        /// True if input actions is enabled.
        /// </returns>
        bool IsEnabled { get; }
    }
}
