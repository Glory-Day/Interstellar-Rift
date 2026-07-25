using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Results for connectable <see cref="Slot"/> searched with <see cref="ModuleSearcher"/>.
    /// </summary>
    public readonly struct SearchedSlotResult
    {
        //TODO: Radius is a constant that never changes after being set, so passing it through this struct on every search is unnecessary.
        //      Once ElectricArcController becomes a LocalServiceBehaviour, it should receive ModuleSearcher via Initialize() and read Radius from it directly.
        /// <summary>
        /// Radius for searching <see cref="Slot"/>.
        /// </summary>
        public readonly float Radius;

        /// <summary>
        /// The service that provides the functionality to attach a searched module.
        /// </summary>
        public readonly ModuleConnector Connector;

        /// <summary>
        /// The searched connectable <see cref="Slot"/>.
        /// </summary>
        public readonly Slot Slot;

        /// <param name="radius">Radius for searching <see cref="Slot"/>.</param>
        /// <param name="connector">The service that provides the functionality to attach a searched module.</param>
        /// <param name="slot">The searched connectable <see cref="Slot"/>.</param>
        public SearchedSlotResult(float radius, ModuleConnector connector, Slot slot)
        {
            Radius = radius;

            Connector = connector;
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
