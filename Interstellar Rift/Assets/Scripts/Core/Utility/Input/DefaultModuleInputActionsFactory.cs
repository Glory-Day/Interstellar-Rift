namespace Core.Utility.Input
{
    /// <summary>
    /// An <see cref="IGameInputActionsFactory"/> that creates <see cref="DefaultModuleInputActions"/> instances.
    /// </summary>
    public class DefaultModuleInputActionsFactory : IGameInputActionsFactory
    {
        private readonly ModuleInputActions _inputActions;

        /// <param name="inputActions">The generated <see cref="ModuleInputActions"/> to pass to created instances.</param>
        public DefaultModuleInputActionsFactory(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        /// <inheritdoc/>
        public IGameInputActions Create()
        {
            return new DefaultModuleInputActions(_inputActions);
        }
    }
}
