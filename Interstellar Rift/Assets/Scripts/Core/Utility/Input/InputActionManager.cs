using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using VContainer.Unity;

namespace Core.Utility.Input
{
    public class InputActionManager : IInitializable, IDisposable
    {
        private readonly List<IGameInputActions> _gameInputActions = new List<IGameInputActions>();
        private readonly List<IUIInputActions> _uiInputActions = new List<IUIInputActions>();

        private readonly IEnumerable<IGameInputActionsFactory> _gameInputActionsFactories;
        private readonly IEnumerable<IUIInputActionsFactory> _uiInputActionsFactories;

        public InputActionManager(IEnumerable<IGameInputActionsFactory> gameInputActionsFactories,
                                  IEnumerable<IUIInputActionsFactory> uiInputActionsFactories)
        {
            _gameInputActionsFactories = gameInputActionsFactories;
            _uiInputActionsFactories = uiInputActionsFactories;
        }

        public void Initialize()
        {
            _gameInputActionsFactories.ForEach(factory => _gameInputActions.Add(factory.Create()));
            _uiInputActionsFactories.ForEach(factory => _uiInputActions.Add(factory.Create()));

            SwitchMode(Mode);
        }

        public void SwitchMode(InputMode mode)
        {
            switch (mode)
            {
                case InputMode.Game:
                    EnableGameInputActions();
                    DisableUIInputActions();
                    break;
                case InputMode.UI:
                    DisableGameInputActions();
                    EnableUIInputActions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public void Dispose()
        {
            DisableGameInputActions();
            DisableUIInputActions();
        }

        private void EnableGameInputActions()
        {
            foreach (var gameInputAction in _gameInputActions)
            {
                gameInputAction.Enable();
            }
        }

        private void DisableGameInputActions()
        {
            foreach (var gameInputAction in _gameInputActions)
            {
                gameInputAction.Disable();
            }
        }

        private void EnableUIInputActions()
        {
            foreach (var uiInputAction in _uiInputActions)
            {
                uiInputAction.Enable();
            }
        }

        private void DisableUIInputActions()
        {
            foreach (var uiInputAction in _uiInputActions)
            {
                uiInputAction.Disable();
            }
        }

        public InputMode Mode { get; private set; } = InputMode.Game;
    }
}
