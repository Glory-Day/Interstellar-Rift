using System;
using Core.Object.Module;
using UnityEngine;

namespace Core.Utility.Extension
{
    public static class Rank_Extension
    {
        public static IColorProvider ToColorProvider(this Rank rank)
        {
            return rank switch
            {
                Rank.Alpha   => new FixedColorProvider(new Color(57  / 255f, 255 / 255f, 20  / 255f)),
                Rank.Beta    => new FixedColorProvider(new Color(255 / 255f, 215 / 255f, 0   / 255f)),
                Rank.Gamma   => new FixedColorProvider(new Color(255 / 255f, 102 / 255f, 0   / 255f)),
                Rank.Delta   => new FixedColorProvider(new Color(255 / 255f, 34  / 255f, 34  / 255f)),
                Rank.Epsilon => new FixedColorProvider(new Color(255 / 255f, 102 / 255f, 204 / 255f)),
                Rank.Zeta    => new FixedColorProvider(new Color(204 / 255f, 68  / 255f, 255 / 255f)),
                Rank.Eta     => new FixedColorProvider(new Color(68  / 255f, 136 / 255f, 255 / 255f)),
                Rank.Theta   => new FixedColorProvider(new Color(68  / 255f, 221 / 255f, 170 / 255f)),
                Rank.Iota    => new FixedColorProvider(new Color(0   / 255f, 255 / 255f, 170 / 255f)),
                Rank.Kappa   => new FixedColorProvider(new Color(232 / 255f, 232 / 255f, 208 / 255f)),
                Rank.Lambda  => new GradientColorProvider(Color.white),
                _            => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
            };
        }
    }
}
