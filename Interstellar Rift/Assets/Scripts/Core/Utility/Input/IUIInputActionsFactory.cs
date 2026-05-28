namespace Core.Utility.Input
{
    public interface IUIInputActionsFactory
    {
        /// <summary>
        /// Creates an input actions instance that is only used while the UI is active.
        /// </summary>
        /// <returns>The created input actions instance.</returns>
        IUIInputActions Create();
    }
}
