using System;
using Sirenix.OdinInspector;

namespace Core.Object.Module
{
    [Serializable]
    public class ModuleData
    {
        [ShowInInspector] public float Mass { get; set; }
        [ShowInInspector] public float Durability { get; set; }
    }
}
