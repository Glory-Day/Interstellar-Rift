using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// Controls the visual effect of an electric arc, using the configured settings to keep its curvature and jitter looking visually natural.
    /// </summary>
    public class ElectricArcVisualEffectInitializer : LocalClientBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [Title("Positions", HorizontalLine = false)]
        [SerializeField] private Transform a;
        [SerializeField] private Transform b;
        [SerializeField] private Transform c;
        [SerializeField] private Transform d;

        #endregion

        #region LOCAL SERVICE API

        private ElectricArcVisualEffect _electricArcVisualEffect;

        #endregion

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(a.position, 0.05f);
            Gizmos.DrawWireSphere(b.position, 0.05f);
            Gizmos.DrawWireSphere(c.position, 0.05f);
            Gizmos.DrawWireSphere(d.position, 0.05f);

            Gizmos.DrawLine(a.position, b.position);
            Gizmos.DrawLine(b.position, c.position);
            Gizmos.DrawLine(c.position, d.position);
        }

#endif

        public override void Install()
        {
            Console.LogProgress();

            _electricArcVisualEffect = Resolver.GetLocalService<ElectricArcVisualEffectBuilder>()
                                               .WithTransforms(a, b, c, d)
                                               .WithGameObject(gameObject)
                                               .Build();

            var installer = Resolver.GetLocalService<DynamicLocalServiceInstaller>();
            installer.Install(_electricArcVisualEffect);

            base.Install();
        }
    }
}
