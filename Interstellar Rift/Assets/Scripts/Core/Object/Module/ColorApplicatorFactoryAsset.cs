using System;
using Core.Object.Service;
using Core.Utility.Extension;
using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Color Applicator Factory Asset",
                     menuName = "Assets/Services/Local/Color Applicator")]
    public class ColorApplicatorFactoryAsset : LocalServiceFactoryAsset
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [SerializeField][Range(0f, 5f)] private float speed = 1f;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var model = resolver.GetLocalService<ModuleModelResolver>().Model;
            var rank = model.Rank;

            LocalService service = rank switch
            {
                ModuleRank.Alpha   => new FixedColorApplicator(new Color(57  / 255f, 255 / 255f, 20  / 255f), resolver),
                ModuleRank.Beta    => new FixedColorApplicator(new Color(255 / 255f, 215 / 255f, 0   / 255f), resolver),
                ModuleRank.Gamma   => new FixedColorApplicator(new Color(255 / 255f, 102 / 255f, 0   / 255f), resolver),
                ModuleRank.Delta   => new FixedColorApplicator(new Color(255 / 255f, 34  / 255f, 34  / 255f), resolver),
                ModuleRank.Epsilon => new FixedColorApplicator(new Color(255 / 255f, 102 / 255f, 204 / 255f), resolver),
                ModuleRank.Zeta    => new FixedColorApplicator(new Color(204 / 255f, 68  / 255f, 255 / 255f), resolver),
                ModuleRank.Eta     => new FixedColorApplicator(new Color(68  / 255f, 136 / 255f, 255 / 255f), resolver),
                ModuleRank.Theta   => new FixedColorApplicator(new Color(68  / 255f, 221 / 255f, 170 / 255f), resolver),
                ModuleRank.Iota    => new FixedColorApplicator(new Color(0   / 255f, 255 / 255f, 170 / 255f), resolver),
                ModuleRank.Kappa   => new FixedColorApplicator(new Color(232 / 255f, 232 / 255f, 208 / 255f), resolver),
                ModuleRank.Lambda  => new GradientColorApplicator(speed, resolver),
                _                  => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
            };

            Console.LogSuccess($"{nameof(ColorApplicator).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return service;
        }
    }
}
