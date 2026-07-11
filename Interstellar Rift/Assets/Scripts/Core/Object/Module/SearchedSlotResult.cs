using UnityEngine;

namespace Core.Object.Module
{
    public readonly struct SearchedSlotResult
    {
        public readonly float Radius;

        public readonly ModuleConnector Connector;
        public readonly Slot Slot;

        public SearchedSlotResult(float radius, ModuleConnector connector, Slot slot)
        {
            Radius = radius;

            Connector = connector;
            Slot = slot;
        }

        public Vector3 Position => Slot.transform.position;

        public Vector3 Outward => Slot.transform.up;

        public Direction Direction => Slot.Direction;
    }
}
