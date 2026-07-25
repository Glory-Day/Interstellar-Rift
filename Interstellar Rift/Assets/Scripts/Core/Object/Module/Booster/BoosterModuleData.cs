using System;
using Sirenix.OdinInspector;

namespace Core.Object.Module.Booster
{
    [Serializable]
    public class BoosterModuleData : ModuleData
    {
        [ShowInInspector] public float Trust { get; set; }
    }
}
