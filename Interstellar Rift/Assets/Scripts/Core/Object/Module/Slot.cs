using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class Slot : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [SerializeField] private SlotState state;
        [SerializeField] private Direction direction;

        #endregion

        public ModuleConnector Connector { get; set; }

        public SlotState State { get => state; set => state = value; }

        public Direction Direction => direction;
    }
}
