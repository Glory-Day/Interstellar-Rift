using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleTestbed : MonoBehaviour
    {
        #region SERIALIZABLE PROPERTY API

        [Title("Data")]
        [ShowInInspector] private ModuleData Data { get; set; }

        #endregion
    }
}
