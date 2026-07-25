using System;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Creates an appropriate <see cref="IColorApplicator"/> for the given <see cref="ModuleRank"/>.
    /// For most ranks, it provides a fixed color specific to that rank; for the <see cref="ModuleRank.Lambda"/> rank, it provides an animated gradient applicator.
    /// </summary>
    public class ColorApplicatorFactory : IFactory<IColorApplicator>
    {
        #region SERVICE FIELD API

        private readonly UpdateEventHandler _updateEventHandler;

        #endregion

        private readonly ModuleRank _rank;

        /// <param name="rank">The <see cref="ModuleRank"/> to create a color applicator for.</param>
        /// <param name="updateEventHandler">The <see cref="UpdateEventHandler"/> service to inject.</param>
        public ColorApplicatorFactory(ModuleRank rank, UpdateEventHandler updateEventHandler)
        {
            _rank = rank;

            _updateEventHandler = updateEventHandler;
        }

        public IColorApplicator Create()
        {
            return _rank switch
            {
                ModuleRank.Alpha   => new FixedColorApplicator(new Color(57  / 255f, 255 / 255f, 20  / 255f)),
                ModuleRank.Beta    => new FixedColorApplicator(new Color(255 / 255f, 215 / 255f, 0   / 255f)),
                ModuleRank.Gamma   => new FixedColorApplicator(new Color(255 / 255f, 102 / 255f, 0   / 255f)),
                ModuleRank.Delta   => new FixedColorApplicator(new Color(255 / 255f, 34  / 255f, 34  / 255f)),
                ModuleRank.Epsilon => new FixedColorApplicator(new Color(255 / 255f, 102 / 255f, 204 / 255f)),
                ModuleRank.Zeta    => new FixedColorApplicator(new Color(204 / 255f, 68  / 255f, 255 / 255f)),
                ModuleRank.Eta     => new FixedColorApplicator(new Color(68  / 255f, 136 / 255f, 255 / 255f)),
                ModuleRank.Theta   => new FixedColorApplicator(new Color(68  / 255f, 221 / 255f, 170 / 255f)),
                ModuleRank.Iota    => new FixedColorApplicator(new Color(0   / 255f, 255 / 255f, 170 / 255f)),
                ModuleRank.Kappa   => new FixedColorApplicator(new Color(232 / 255f, 232 / 255f, 208 / 255f)),
                ModuleRank.Lambda  => new GradientColorApplicator(_updateEventHandler),
                _                  => throw new ArgumentOutOfRangeException(nameof(_rank), _rank, null)
            };
        }
    }
}
