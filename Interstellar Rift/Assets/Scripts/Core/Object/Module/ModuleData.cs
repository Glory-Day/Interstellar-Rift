using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Data", menuName =  "Scriptable Objects/Data/Module/Test")]
    public class ModuleData : ScriptableObject
    {
        #region SERIALIZABLE PROPERTY API

        [Title("Default")]
        [ShowInInspector] public Rank Rank { get; set; }
        [ShowInInspector] public float Mass { get; set; }

        #endregion
    }
}
