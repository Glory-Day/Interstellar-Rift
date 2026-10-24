using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// Controls the visual effect of an electric arc, using the configured settings to keep its curvature and jitter looking visually natural.
    /// </summary>
    public class ElectricArcVisualEffectInstaller : LocalServiceInstallationBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [SerializeField] private Color color;

        [Title("References")]
        [SerializeField] private LineRenderer lineRenderer;

        [Title("Object")]
        [SerializeField] private GameObject[] sparkEffectObjects;

        #endregion

        #region LOCAL SERVICE API

        private ElectricArcVisualEffect _electricArcVisualEffect;

        #endregion

        private void OnDestroy()
        {
            Console.LogProgress();

            _electricArcVisualEffect = null;
        }

        public override void Install()
        {
            Console.LogProgress();

            var material = lineRenderer.material;
            material.SetColor(Shader.PropertyToID("_Color"), color);

            _electricArcVisualEffect = Resolver.GetLocalService<ElectricArcVisualEffectBuilder>()
                                               .WithGameObject(gameObject)
                                               .WithSparkEffectObjects(sparkEffectObjects)
                                               .WithLineRenderer(lineRenderer)
                                               .Build();

            var installer = Resolver.GetLocalService<DynamicLocalServiceInstaller>();
            installer.Install(_electricArcVisualEffect);

            var uninstaller = Resolver.GetLocalService<DynamicLocalServiceUninstaller>();
            uninstaller.Uninstall<ElectricArcVisualEffectBuilder>();

            base.Install();
        }

        public override InstallationMode Mode => InstallationMode.Automatic;
    }
}
