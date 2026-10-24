using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> asset that provides the basic feature for registering a specific global service.
    /// </summary>
    public abstract class GlobalServiceRegistrarAsset : ScriptableObject, IGlobalServiceRegistrable
    {
        /// <inheritdoc/>
        public abstract void Register(IContainerBuilder builder);
    }
}

