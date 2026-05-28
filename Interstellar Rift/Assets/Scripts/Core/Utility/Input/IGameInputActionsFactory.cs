namespace Core.Utility.Input
{
    public interface IGameInputActionsFactory
    {
        /// <summary>
        /// Create an input actions instance that is only used during gameplay.
        /// </summary>
        /// <returns>The created input actions instance.</returns>
        IGameInputActions Create();
    }
}
