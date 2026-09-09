using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Data Table",
                     menuName = "Assets/Data/Module Data Table")]
    public class ModuleDataTable : SerializedScriptableObject
    {
        [DictionaryDrawerSettings(KeyLabel = "Rank", ValueLabel = "Data")]
        [SerializeField] private Dictionary<ModuleRank, ModuleData> table = new Dictionary<ModuleRank, ModuleData>();

        public ModuleData this[ModuleRank rank] => table[rank];
    }
}
