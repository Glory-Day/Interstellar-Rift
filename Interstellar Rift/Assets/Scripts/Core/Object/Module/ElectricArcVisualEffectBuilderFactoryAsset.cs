using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Electric Arc Visual Effect Builder Factory Asset",
                     menuName = "Assets/Services/Local/Electric Arc Visual Effect Builder")]
    public class ElectricArcVisualEffectBuilderFactoryAsset : LocalServiceBuilderFactoryAsset
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("Number of line segments the connection path is split into. Higher values trace the curve more smoothly.")]
        [Range(4, 64)]
        [SerializeField] private int segments = 24;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var moduleConnectionPathCacher = resolver.GetLocalService<ModuleConnectionPathCacher>();

            Console.LogSuccess($"{nameof(ElectricArcVisualEffectBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ElectricArcVisualEffectBuilder(segments,
                                                      moduleConnectionPathCacher);
        }

        public override InstallationMode Mode => InstallationMode.Automatic;
    }
}
