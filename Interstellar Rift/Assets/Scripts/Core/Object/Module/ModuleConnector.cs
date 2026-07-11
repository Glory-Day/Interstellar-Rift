using System.Collections.Generic;
using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleConnector : LocalServiceBehaviour, INode
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [DictionaryDrawerSettings(KeyLabel = "Direction", ValueLabel = "Slot")]
        [SerializeField] private List<Slot> slots;
        [SerializeField] private Slot joint;

        #endregion

        public void Connect(Direction direction, ModuleConnector connector)
        {
            var count = slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (slots[i].Direction != direction)
                {
                    continue;
                }

                slots[i].Connector = connector;
                slots[i].State = SlotState.Unattachable;
            }
        }

        public void Disconnect(Direction direction)
        {
            var count = slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (slots[i].Direction != direction)
                {
                    continue;
                }

                slots[i].Connector = null;
                slots[i].State = SlotState.Attachable;
            }
        }

        public INode GetChild(Direction direction)
        {
            var count = slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (slots[i].Direction == direction)
                {
                    return slots[i].Connector;
                }
            }

            return null;
        }

        public bool HasChild(Direction direction)
        {
            var count = slots.Count;
            for (var i = 0; i < count; i++)
            {
                if (slots[i].Direction == direction)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryGetChild(Direction direction, out INode child)
        {
            child = GetChild(direction);

            return child != null;
        }

        public List<Slot> Slots => slots;

        public Slot Joint => joint;

        public INode Parent => joint.Connector;

        public bool IsRoot => joint == null;
    }
}
