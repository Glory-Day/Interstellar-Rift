using Core.Object.Service;
using Core.Utility.Extension;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Electric Arc Visual Effect Builder Factory Asset",
                     menuName = "Assets/Services/Local/Electric Arc Visual Effect Builder")]
    public class ElectricArcVisualEffectBuilderFactoryAsset : LocalServiceFactoryAsset
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("Normalized [0, 1] strength of the arc's sag/curvature and jitter displacement, scaled by the distance between the start and end points. Higher values produce a more pronounced curve and jitter.")]
        [Range(0f, 1f)]
        [SerializeField] private float amount = 0.3f;

        [Title("Jitter", HorizontalLine = false)]
        [Tooltip("Playback speed of the Perlin noise sampling used for jitter. Higher values make the arc flicker/wobble faster.")]
        [Range(0f, 5f)]
        [SerializeField] private float speed = 2f;
        [Tooltip("Maximum deviation applied to a jittered point's Bézier parameter (t) around its base ratio; the result is then clamped to [0, 1].")]
        [Range(0f, 0.2f)]
        [SerializeField] private float range = 0.05f;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ElectricArcVisualEffectBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ElectricArcVisualEffectBuilder(amount, speed, range, resolver);
        }
    }
}
