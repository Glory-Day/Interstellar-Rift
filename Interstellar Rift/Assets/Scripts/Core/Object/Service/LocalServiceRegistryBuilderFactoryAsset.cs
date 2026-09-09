using System.Collections.Generic;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Service
{
    [CreateAssetMenu(fileName = "Local Service Registry Builder Factory Asset",
                     menuName = "Assets/Services/Local Service Registry Builder")]
    public class LocalServiceRegistryBuilderFactoryAsset : ScriptableObject, IFactory<LocalServiceRegistryBuilder>
    {
        #region SERIALIZABLE FIELD API

        [Title("Assets")]
        [SerializeField] private List<LocalServiceFactoryAsset> assets = new List<LocalServiceFactoryAsset>();

        #endregion

        public LocalServiceRegistryBuilder Create()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(LocalServiceRegistryBuilder).ToNicifyPascalCase().ToBoldStyle()} is created.");

            return new LocalServiceRegistryBuilder(assets);
        }
    }
}
