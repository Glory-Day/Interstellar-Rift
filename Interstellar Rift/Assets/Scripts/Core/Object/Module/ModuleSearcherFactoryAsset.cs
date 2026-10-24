using Core.Object.Service;
using Core.Utility.Extensions;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    [CreateAssetMenu(fileName = "Module Searcher Factory Asset",
                     menuName = "Assets/Services/Local/Module Searcher")]
    public class ModuleSearcherFactoryAsset : LocalServiceFactoryAsset
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("Radius for searching modules.")]
        [SerializeField] private float range;
        [Tooltip("The layer mask of modules to search for.")]
        [SerializeField] private LayerMask target;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            var localServiceRepository = resolver.GetGlobalService<LocalServiceRepository>();

            var moduleSocket = resolver.GetLocalService<ModuleSocket>();

            var transform = resolver.GetLocalService<ModuleTransformResolver>().Main;

            Console.LogSuccess($"{nameof(ModuleSearcher).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleSearcher(range, target,
                                      localServiceRepository,
                                      moduleSocket,
                                      transform);
        }

        public override InstallationMode Mode => InstallationMode.Lazy;
    }
}
