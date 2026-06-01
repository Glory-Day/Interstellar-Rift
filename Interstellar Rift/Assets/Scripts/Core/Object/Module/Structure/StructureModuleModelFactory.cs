using System;

namespace Core.Object.Module.Structure
{
    public class StructureModuleModelFactory
    {
        public ModuleModel Create(string name, ModuleRank rank, ModuleData data)
        {
            return name switch
            {
                ModuleNames.Structure.CoreModule => new CoreModuleModel(rank, data),
                ModuleNames.Structure.ConnectionModule => new ConnectionModuleModel(rank, data),
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
            };
        }
    }
}
