using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Represents a slot that provides the connection functionality needed to attach a module.
    /// </summary>
    public class Slot : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("The slot's current attachment state.")]
        [SerializeField] private SlotState state;
        [Tooltip("The direction of the slot attached to the module.")]
        [SerializeField] private Direction direction;

        #endregion

        /// <summary>
        /// The service that provides the functionality to attach a module.
        /// </summary>
        public ModuleConnector Connector { get; set; }

        /// <summary>
        /// The slot's current attachment state.
        /// </summary>
        public SlotState State { get => state; set => state = value; }

        /// <summary>
        /// The slot's fixed direction, as configured in the Inspector.
        /// Used as a reference to derive the slot's direction relative to other attached modules.
        /// </summary>
        public Direction Direction => direction;
    }
}
