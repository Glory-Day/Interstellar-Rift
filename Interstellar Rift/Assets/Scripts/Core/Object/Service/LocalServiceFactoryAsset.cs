using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Service
{
    public abstract class LocalServiceFactoryAsset : ScriptableObject, ILocalServiceFactory
    {
        #region SERIALIZABLE FIELD API

        [Title("Service Installation")]
        [ReadOnly]
        [ShowInInspector]
        public abstract InstallationMode Mode { get; }

        #endregion

        public abstract LocalService Create(ServiceResolver resolver);
    }
}

