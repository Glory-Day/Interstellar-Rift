using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Data", menuName =  "Scriptable Objects/Data/Module/Base")]
    public class ModuleDataTable : ScriptableObject
    {
        [DictionaryDrawerSettings(KeyLabel = "Rank", ValueLabel = "Data")]
        [ShowInInspector] private Dictionary<ModuleRank, ModuleData> table;

        public ModuleData this[ModuleRank rank] => table[rank];
    }
}
