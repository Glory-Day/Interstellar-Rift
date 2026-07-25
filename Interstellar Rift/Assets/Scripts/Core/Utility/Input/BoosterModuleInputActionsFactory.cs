namespace Core.Utility.Input
{
    public class BoosterModuleInputActionsFactory : IGameInputActionsFactory
    {
        private readonly ModuleInputActions _inputActions;

        public BoosterModuleInputActionsFactory(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public IGameInputActions Create()
        {
            return new BoosterModuleInputActions(_inputActions);
        }
    }
}
