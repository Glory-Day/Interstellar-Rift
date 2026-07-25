using System.Collections;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object
{
    /// <summary>
    /// A global service that provides coroutine functionality, implementing <see cref="ICoroutineRunner"/> by forwarding calls to Unity's built-in coroutine methods.
    /// </summary>
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void OnDestroy()
        {
            Console.LogProgress();

            StopAllCoroutines();
        }

        /// <inheritdoc/>
        void ICoroutineRunner.StartCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        /// <inheritdoc/>
        void ICoroutineRunner.StopCoroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }

        /// <inheritdoc/>
        void ICoroutineRunner.StopAllCoroutines()
        {
            StopAllCoroutines();
        }
    }
}
