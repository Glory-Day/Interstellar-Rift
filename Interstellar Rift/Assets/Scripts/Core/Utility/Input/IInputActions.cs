namespace Core.Utility.Input
{
    /// <summary>
    /// Represents a set of input actions that can be enabled or disabled.
    /// </summary>
    public interface IInputActions
    {
        /// <summary>
        /// Enables the input actions.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the input actions.
        /// </summary>
        void Disable();

        /// <summary>
        /// Gets whether the input actions are currently enabled.
        /// </summary>
        bool IsEnabled { get; }
    }
}
