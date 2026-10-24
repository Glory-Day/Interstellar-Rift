using System.Collections.Generic;
using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to attach a module to an attachable slot.
    /// </summary>
    public class ModuleSocket : LocalService, INode
    {
        #region LOCAL SERVICE API

        private Transform _transform;

        #endregion

        public ModuleSocket(List<Slot> slots, Slot joint,
                            Transform transform)
        {
            Console.LogProgress();

            Slots = slots;
            Joint = joint;

            _transform = transform;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            Slots = null;
            Joint = null;

            _transform = null;
        }

        /// <summary>
        /// Attaches a module to the slot at the given direction.
        /// </summary>
        /// <param name="socket">The <see cref="ModuleSocket"/> of the module to attach.</param>
        /// <param name="direction">The direction of the slot configured on the module.</param>
        public void Connect(ModuleSocket socket, Direction direction)
        {
            Console.LogProgress();

            var count = Slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (Slots[i].Direction != direction)
                {
                    continue;
                }

                Slots[i].Connect(socket);
                socket.Attach(Slots[i]);
            }
        }

        private void Attach(Slot slot)
        {
            Console.LogProgress();

            _transform.SetParent(slot.transform, true);
        }

        public bool Disconnect()
        {
            Console.LogProgress();

            if (IsRootNode || Joint.Socket == null)
            {
                return false;
            }

            Joint.Socket.Disconnect(this);

            return true;
        }

        private void Disconnect(ModuleSocket socket)
        {
            Console.LogProgress();

            var count = Slots.Count;
            for (var i = 0; i < count; i++)
            {
                var slot = Slots[i];
                if (slot.Socket != socket)
                {
                    continue;
                }

                var child = slot.Socket;
                if (child == null)
                {
                    return;
                }

                slot.Disconnect();

                child.Joint.Disconnect();
                child.Detach();

                return;
            }
        }

        private void Detach()
        {
            Console.LogProgress();

            _transform.SetParent(null, true);
        }

        /// <inheritdoc/>
        public INode GetChildNode(Direction direction)
        {
            Console.LogProgress();

            var count = Slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (Slots[i].Direction == direction)
                {
                    return Slots[i].Socket;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public bool HasChildNode(Direction direction)
        {
            Console.LogProgress();

            var count = Slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (Slots[i].Direction == direction)
                {
                    return Slots[i].Socket != null;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public bool TryGetChildNode(Direction direction, out INode child)
        {
            Console.LogProgress();

            child = GetChildNode(direction);

            return child != null;
        }

        public bool IsAncestorOf(ModuleSocket socket)
        {
            for (INode node = socket; node != null; node = node.ParentNode)
            {
                if (ReferenceEquals(node, this))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public INode ParentNode => IsRootNode ? null : Joint.Socket;

        /// <inheritdoc/>
        public bool IsRootNode => Joint == null;

        public List<Slot> Slots { get; private set; }

        public Slot Joint { get; private set; }
    }
}
