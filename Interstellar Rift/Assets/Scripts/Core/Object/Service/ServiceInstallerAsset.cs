using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    public abstract class ServiceInstallerAsset : ScriptableObject
    {
        public abstract void Install(IContainerBuilder builder);
    }
}
