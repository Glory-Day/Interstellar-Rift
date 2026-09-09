using UnityEngine;

namespace Core.Object.Service
{
    public abstract class LocalServiceFactoryAsset : ScriptableObject, ILocalServiceFactory
    {
        public abstract LocalService Create(ServiceResolver resolver);
    }
}

