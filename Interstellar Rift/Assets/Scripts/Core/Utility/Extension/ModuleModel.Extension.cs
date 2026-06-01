using Core.Object.Module;
using Core.Object.Module.Structure;
using UnityEngine;

namespace Core.Utility.Extension
{
    public static class ModuleModel_Extension
    {
        public static IColorProvider ToColorProvider(this ModuleModel model)
        {
            return model switch
            {
                CoreModuleModel => new FixedColorProvider(new Color(255 / 255f, 0 / 255f, 0 / 255f)),
                _               => model.Rank.ToColorProvider()
            };
        }
    }
}
