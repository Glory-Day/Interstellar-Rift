using System;
using Core.Object.Module;
using UnityEngine;

namespace Core.Utility.Extension
{
    public static class ModuleRank_Extension
    {
        public static IColorProvider ToColorProvider(this ModuleRank rank)
        {
            return rank switch
            {
                ModuleRank.Alpha   => new FixedColorProvider(new Color(57  / 255f, 255 / 255f, 20  / 255f)),
                ModuleRank.Beta    => new FixedColorProvider(new Color(255 / 255f, 215 / 255f, 0   / 255f)),
                ModuleRank.Gamma   => new FixedColorProvider(new Color(255 / 255f, 102 / 255f, 0   / 255f)),
                ModuleRank.Delta   => new FixedColorProvider(new Color(255 / 255f, 34  / 255f, 34  / 255f)),
                ModuleRank.Epsilon => new FixedColorProvider(new Color(255 / 255f, 102 / 255f, 204 / 255f)),
                ModuleRank.Zeta    => new FixedColorProvider(new Color(204 / 255f, 68  / 255f, 255 / 255f)),
                ModuleRank.Eta     => new FixedColorProvider(new Color(68  / 255f, 136 / 255f, 255 / 255f)),
                ModuleRank.Theta   => new FixedColorProvider(new Color(68  / 255f, 221 / 255f, 170 / 255f)),
                ModuleRank.Iota    => new FixedColorProvider(new Color(0   / 255f, 255 / 255f, 170 / 255f)),
                ModuleRank.Kappa   => new FixedColorProvider(new Color(232 / 255f, 232 / 255f, 208 / 255f)),
                ModuleRank.Lambda  => new GradientColorProvider(Color.white),
                _                  => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
            };
        }
    }
}
