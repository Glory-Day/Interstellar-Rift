namespace Core.Utility.Input
{
    public class BoosterInputActionsFactory : IGameInputActionsFactory
    {
        private readonly ModuleInputActions _inputActions;

        public BoosterInputActionsFactory(ModuleInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public IGameInputActions Create()
        {
            return new BoosterInputActions(_inputActions);
        }
    }
}
