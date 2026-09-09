using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    public abstract class GlobalServiceRegistrarAsset : ScriptableObject, IGlobalServiceRegistrable
    {
        public abstract void Register(IContainerBuilder builder);
    }
}

