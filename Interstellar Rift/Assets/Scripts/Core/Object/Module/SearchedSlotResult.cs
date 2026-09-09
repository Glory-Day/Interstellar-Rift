using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Results for connectable <see cref="Slot"/> searched with <see cref="ModuleSearcher"/>.
    /// </summary>
    public readonly struct SearchedSlotResult
    {
        /// <summary>
        /// The service that provides the functionality to attach a searched module.
        /// </summary>
        public readonly ModuleSocket Socket;

        /// <summary>
        /// The searched connectable <see cref="Slot"/>.
        /// </summary>
        public readonly Slot Slot;

        /// <param name="socket">The service that provides the functionality to attach a searched module.</param>
        /// <param name="slot">The searched connectable <see cref="Slot"/>.</param>
        public SearchedSlotResult(ModuleSocket socket, Slot slot)
        {
            Socket = socket;
            Slot = slot;
        }

        /// <summary>
        /// Position of the searched connectable <see cref="Slot"/>.
        /// </summary>
        public Vector3 Position => Slot.transform.position;

        /// <summary>
        /// The outward-facing direction of the searched connectable <see cref="Slot"/>.
        /// </summary>
        public Vector3 Outward => Slot.transform.up;

        /// <summary>
        /// <see cref="Direction"/> of the searched connectable <see cref="Slot"/>.
        /// </summary>
        public Direction Direction => Slot.Direction;
    }
}
