namespace Core.Utility.Input
{
    /// <summary>
    /// An <see cref="IGameInputActionsFactory"/> that creates <see cref="CameraInputActions"/> instances.
    /// </summary>
    public class CameraInputActionsFactory : IGameInputActionsFactory
    {
        private readonly MapInputActions _inputActions;

        /// <param name="inputActions">The generated <see cref="MapInputActions"/> to pass to created instances.</param>
        public CameraInputActionsFactory(MapInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        /// <inheritdoc/>
        public IGameInputActions Create()
        {
            return new CameraInputActions(_inputActions);
        }
    }
}
