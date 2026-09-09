using Core.Object.Service;
using Core.Utility.Extension;
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
        [SerializeField] private float radius;
        [Tooltip("The layer mask of modules to search for.")]
        [SerializeField] private LayerMask target;

        #endregion

        public override LocalService Create(ServiceResolver resolver)
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleSearcher).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new ModuleSearcher(radius, target, resolver);
        }
    }
}
