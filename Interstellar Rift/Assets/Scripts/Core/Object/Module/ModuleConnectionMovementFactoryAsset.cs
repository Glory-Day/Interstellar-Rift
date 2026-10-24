using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Connection Movement Factory Asset",
                     menuName = "Assets/Services/Local/Module Connection Movement")]
    public class ModuleConnectionMovementFactoryAsset : LocalServiceFactoryAsset
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Range(0f, 30f)]
        [SerializeField] private float speed = 12f;
        [Range(0f, 1f)]
        [SerializeField] private float duration;
        [SerializeField] private ModuleConnectionMovement.Alignment alignment;
        [SerializeField] private AnimationCurve easing;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var moduleConnectionPathCacher = resolver.GetLocalService<ModuleConnectionPathCacher>();
            var moduleSocket = resolver.GetLocalService<ModuleSocket>();

            var transform = resolver.GetLocalService<ModuleTransformResolver>().Main;

            Console.LogSuccess($"{nameof(ModuleConnectionMovementFactoryAsset).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleConnectionMovement(speed, duration,
                                                alignment,
                                                easing,
                                                moduleConnectionPathCacher, moduleSocket,
                                                transform);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
