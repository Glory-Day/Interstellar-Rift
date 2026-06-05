using System;
using UnityEngine;

namespace Core.Object
{
    [DisallowMultipleComponent]
    public class ObjectPoolReleaser : MonoBehaviour
    {
        private void OnDestroy()
        {
            OnRelease = null;
        }

        /// <summary>
        /// Releases this object back to the pool.
        /// </summary>
        public void Release()
        {
            OnRelease?.Invoke();
        }

        /// <summary>
        /// Occurs when this object is released back to the pool.
        /// </summary>
        internal event Action OnRelease;
    }
}
