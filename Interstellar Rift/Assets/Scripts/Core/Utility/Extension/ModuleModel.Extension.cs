using System;
using Core.Object.Module;
using Core.Object.Module.Structure;
using UnityEngine;

namespace Core.Utility.Extension
{
    public static class ModuleModel_Extension
    {
        public static IColorApplicator ToColorProvider(this ModuleModel model)
        {
            return model switch
            {
                CoreModuleModel => new FixedColorApplicator(new Color(255 / 255f, 0 / 255f, 0 / 255f)),
                _               => throw new ArgumentOutOfRangeException(nameof(model), model, null)
            };
        }
    }
}
