namespace Core.Utility.Input
{
    public class DefaultModuleInputActionsFactory : IGameInputActionsFactory
    {
        private readonly ModuleInputActions _inputActions;

        public DefaultModuleInputActionsFactory(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public IGameInputActions Create()
        {
            return new DefaultModuleInputActions(_inputActions);
        }
    }
}
