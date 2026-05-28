namespace Core.Utility.Input
{
    public class CameraInputActionsFactory : IGameInputActionsFactory
    {
        private readonly MapInputActions _inputActions;

        public CameraInputActionsFactory(MapInputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public IGameInputActions Create()
        {
            return new CameraInputActions(_inputActions);
        }
    }
}
