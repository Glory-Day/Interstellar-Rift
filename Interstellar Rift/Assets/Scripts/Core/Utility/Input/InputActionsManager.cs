using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using VContainer.Unity;

namespace Core.Utility.Input
{
    /// <summary>
    /// A global service that creates every registered <see cref="IGameInputActions"/> and <see cref="IUIInputActions"/> instance and
    /// switches which group is enabled based on the current <see cref="InputMode"/>.
    /// </summary>
    public class InputActionsManager : IInitializable, IDisposable
    {
        private readonly List<IGameInputActions> _gameInputActions = new List<IGameInputActions>();
        private readonly List<IUIInputActions> _uiInputActions = new List<IUIInputActions>();

        private readonly IEnumerable<IGameInputActionsFactory> _gameInputActionsFactories;
        private readonly IEnumerable<IUIInputActionsFactory> _uiInputActionsFactories;

        public InputActionsManager(IEnumerable<IGameInputActionsFactory> gameInputActionsFactories,
                                   IEnumerable<IUIInputActionsFactory> uiInputActionsFactories)
        {
            _gameInputActionsFactories = gameInputActionsFactories;
            _uiInputActionsFactories = uiInputActionsFactories;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _gameInputActionsFactories.ForEach(factory => _gameInputActions.Add(factory.Create()));
            _uiInputActionsFactories.ForEach(factory => _uiInputActions.Add(factory.Create()));

            SwitchMode(Mode);
        }

        /// <summary>
        /// Enables every <see cref="IGameInputActions"/> instance and disables every <see cref="IUIInputActions"/> instance,
        /// or vice versa, depending on the given mode.
        /// </summary>
        /// <param name="mode">The input mode to switch to.</param>
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

            Mode = mode;
        }

        /// <summary>
        /// Disables every <see cref="IGameInputActions"/> and <see cref="IUIInputActions"/> instance.
        /// </summary>
        public void Dispose()
        {
            DisableGameInputActions();
            DisableUIInputActions();
        }

        /// <summary>
        /// Enables every created <see cref="IGameInputActions"/> instance.
        /// </summary>
        private void EnableGameInputActions()
        {
            foreach (var gameInputAction in _gameInputActions)
            {
                gameInputAction.Enable();
            }
        }

        /// <summary>
        /// Disables every created <see cref="IGameInputActions"/> instance.
        /// </summary>
        private void DisableGameInputActions()
        {
            foreach (var gameInputAction in _gameInputActions)
            {
                gameInputAction.Disable();
            }
        }

        /// <summary>
        /// Enables every created <see cref="IUIInputActions"/> instance.
        /// </summary>
        private void EnableUIInputActions()
        {
            foreach (var uiInputAction in _uiInputActions)
            {
                uiInputAction.Enable();
            }
        }

        /// <summary>
        /// Disables every created <see cref="IUIInputActions"/> instance.
        /// </summary>
        private void DisableUIInputActions()
        {
            foreach (var uiInputAction in _uiInputActions)
            {
                uiInputAction.Disable();
            }
        }

        /// <summary>
        /// Gets the currently active input mode.
        /// </summary>
        public InputMode Mode { get; private set; } = InputMode.Game;
    }
}
