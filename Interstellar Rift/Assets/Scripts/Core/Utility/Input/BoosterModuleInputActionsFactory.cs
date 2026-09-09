namespace Core.Utility.Input
{
    /// <summary>
    /// An <see cref="IGameInputActionsFactory"/> that creates <see cref="BoosterModuleInputActions"/> instances.
    /// </summary>
    public class BoosterModuleInputActionsFactory : IGameInputActionsFactory
    {
        private readonly ModuleInputActions _inputActions;

        /// <param name="inputActions">The generated <see cref="ModuleInputActions"/> to pass to created instances.</param>
        public BoosterModuleInputActionsFactory(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        /// <inheritdoc/>
        public IGameInputActions Create()
        {
            return new BoosterModuleInputActions(_inputActions);
        }
    }
}
