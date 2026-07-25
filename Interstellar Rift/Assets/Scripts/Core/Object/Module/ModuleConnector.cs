using System.Collections.Generic;
using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to attach a module to an attachable slot.
    /// </summary>
    public class ModuleConnector : LocalServiceBehaviour, INode
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [DictionaryDrawerSettings(KeyLabel = "Direction", ValueLabel = "Slot")]
        [Tooltip("The list of slots that provide the connection functionality needed to connect a module.")]
        [SerializeField] private List<Slot> slots;
        [Tooltip("The slot used for this module to connect to another module.")]
        [SerializeField] private Slot joint;

        #endregion

        /// <summary>
        /// Attaches a module to the slot at the given direction.
        /// </summary>
        /// <param name="direction">The direction of the slot configured on the module.</param>
        /// <param name="connector">The <see cref="ModuleConnector"/> of the module to attach.</param>
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

        /// <summary>
        /// Detaches the module from the slot at the given direction.
        /// </summary>
        /// <param name="direction">The direction of the slot configured on the module.</param>
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

        /// <inheritdoc/>
        public INode GetChildNode(Direction direction)
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

        /// <inheritdoc/>
        public bool HasChildNode(Direction direction)
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

        /// <inheritdoc/>
        public bool TryGetChildNode(Direction direction, out INode child)
        {
            child = GetChildNode(direction);

            return child != null;
        }

        /// <summary>
        /// The list of slots that provide the connection functionality needed to connect a module.
        /// </summary>
        public List<Slot> Slots => slots;

        /// <summary>
        /// The slot used for this module to connect to another module.
        /// </summary>
        public Slot Joint => joint;

        /// <inheritdoc/>
        public INode ParentNode => joint.Connector;

        /// <inheritdoc/>
        public bool IsRootNode => joint == null;
    }
}
