using System;
using System.Threading;
using Core.Object.Service;
using Cysharp.Threading.Tasks;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleConnector : LocalService
    {
        #region GLOBAL SERVICE API

        private UpdateEventDispatcher _updateEventDispatcher;

        #endregion

        #region LOCAL SERVICE API

        private ModuleConnectionPathUpdater _moduleConnectionPathUpdater;
        private ModuleConnectionMovement _moduleConnectionMovement;
        private ModuleConnectionVisualEffector _moduleConnectionVisualEffector;

        private ModuleSearcher _moduleSearcher;
        private ModuleSocket _moduleSocket;

        #endregion

        private SearchedSlotResult? _target;
        private CancellationTokenSource _cancellation;

        public ModuleConnector(UpdateEventDispatcher updateEventDispatcher,
                               ModuleConnectionPathUpdater moduleConnectionPathUpdater, ModuleConnectionMovement moduleConnectionMovement, ModuleConnectionVisualEffector moduleConnectionVisualEffector,
                               ModuleSearcher moduleSearcher, ModuleSocket moduleSocket)
        {
            Console.LogProgress();

            _updateEventDispatcher = updateEventDispatcher;

            _moduleConnectionPathUpdater = moduleConnectionPathUpdater;
            _moduleConnectionMovement = moduleConnectionMovement;
            _moduleConnectionVisualEffector = moduleConnectionVisualEffector;

            _moduleSearcher = moduleSearcher;
            _moduleSocket = moduleSocket;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _cancellation?.Cancel();
            _target = null;

            StopSearching();

            _updateEventDispatcher = null;

            _moduleConnectionPathUpdater = null;
            _moduleConnectionMovement = null;
            _moduleConnectionVisualEffector = null;

            _moduleSearcher = null;
            _moduleSocket = null;
        }

        public void StartSearching()
        {
            if (IsSearching || IsConnecting)
            {
                return;
            }

            _updateEventDispatcher.OnUpdate += Search;

            IsSearching = true;
        }

        private void Search()
        {
            _target = _moduleSearcher.Search();

            // Never target a slot in this module's own subtree; attaching there would create a cycle.
            if (_target is not null && _moduleSocket.IsAncestorOf(_target.Value.Socket))
            {
                _target = null;
            }

            if (_target is null)
            {
                _moduleConnectionPathUpdater.Update(null);

                if (_moduleConnectionVisualEffector.IsUpdating)
                {
                    _moduleConnectionVisualEffector.StopVisualEffect();
                }

                return;
            }

            var target = _target.Value;

            // Rotate with the previous frame's path first, then rebuild the path from the joint's rotated position,
            // so the arc's start point stays exactly on the joint this frame.
            _moduleConnectionMovement.Rotate(target);
            _moduleConnectionPathUpdater.Update(_target);

            if (_moduleConnectionVisualEffector.IsUpdating is false)
            {
                _moduleConnectionVisualEffector.StartVisualEffect();
            }
        }

        public void StopSearching()
        {
            if (IsSearching is false)
            {
                return;
            }

            _updateEventDispatcher.OnUpdate -= Search;

            if (_moduleConnectionVisualEffector.IsUpdating)
            {
                _moduleConnectionVisualEffector.StopVisualEffect();
            }

            IsSearching = false;
        }

        public async UniTask<bool> TryConnect()
        {
            Console.LogProgress();

            if (IsSearching || IsConnecting || _target is null)
            {
                return false;
            }

            var target = _target.Value;
            _target = null;
            _cancellation = new CancellationTokenSource();

            IsConnecting = true;

            try
            {
                var arrived = await _moduleConnectionMovement.Move(target, _cancellation.Token);
                if (arrived is false || target.IsAttachable() is false)
                {
                    return false;
                }

                Connect(target);

                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            finally
            {
                _cancellation.Dispose();
                _cancellation = null;

                // Clears the cached path; the service may already be disposed if the connection was cancelled.
                _moduleConnectionPathUpdater?.Update(null);

                IsConnecting = false;
            }
        }

        private void Connect(SearchedSlotResult target)
        {
            var socket = _moduleSocket;
            var joint = _moduleSocket.Joint;

            target.Socket.Connect(socket, target.Direction);
            joint.Connect(target.Socket);
        }

        public bool Disconnect()
        {
            if (IsSearching || IsConnecting)
            {
                return false;
            }

            return _moduleSocket.Disconnect();
        }

        public bool IsSearching { get; private set; }

        public bool IsConnecting { get; private set; }
    }
}
