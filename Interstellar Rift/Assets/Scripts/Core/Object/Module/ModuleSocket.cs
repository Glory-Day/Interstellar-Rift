using System.Collections.Generic;
using Core.Object.Service;
using GloryDay.Debug;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to attach a module to an attachable slot.
    /// </summary>
    public class ModuleSocket : LocalService, INode
    {
        public ModuleSocket(List<Slot> slots, Slot joint, ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            Slots = slots;
            Joint = joint;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            Slots = null;
            Joint = null;
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
            }
        }

        /// <summary>
        /// Detaches the module from the slot at the given direction.
        /// </summary>
        /// <param name="direction">The direction of the slot configured on the module.</param>
        public void Disconnect(Direction direction)
        {
            Console.LogProgress();

            var count = Slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (Slots[i].Direction != direction)
                {
                    continue;
                }

                Slots[i].Disconnect();
            }
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
                    return true;
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

        /// <inheritdoc/>
        public INode ParentNode => Joint.Socket;

        /// <inheritdoc/>
        public bool IsRootNode => Joint == null;

        public List<Slot> Slots { get; private set; }

        public Slot Joint { get; private set; }
    }
}
