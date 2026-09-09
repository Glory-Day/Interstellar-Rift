namespace Core.Utility.Input
{
    /// <summary>
    /// Represents a factory that creates <see cref="IGameInputActions"/> instances.
    /// </summary>
    public interface IGameInputActionsFactory
    {
        /// <summary>
        /// Creates an input actions instance that is only used during gameplay.
        /// </summary>
        /// <returns>The created input actions instance.</returns>
        IGameInputActions Create();
    }
}
