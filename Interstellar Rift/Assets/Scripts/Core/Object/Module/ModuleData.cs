using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleData : ScriptableObject
    {
        #region SERIALIZABLE PROPERTY API

        [Title("Default")]
        [ShowInInspector] public float Mass { get; set; }

        #endregion
    }
}
