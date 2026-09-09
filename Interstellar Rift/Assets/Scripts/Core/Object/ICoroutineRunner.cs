using System.Collections;
using UnityEngine;

namespace Core.Object
{
    /// <summary>
    /// Represents an object that can run coroutines, allowing non-<see cref="MonoBehaviour"/> classes to start and stop coroutines through injection.
    /// </summary>
    public interface ICoroutineRunner
    {
        /// <summary>
        /// Starts the given coroutine.
        /// </summary>
        /// <param name="routine">The coroutine to start.</param>
        public void StartCoroutine(IEnumerator routine);

        /// <summary>
        /// Stops the given coroutine.
        /// </summary>
        /// <param name="routine">The coroutine to stop.</param>
        public void StopCoroutine(Coroutine routine);

        /// <summary>
        /// Stops every coroutine started through this runner.
        /// </summary>
        public void StopAllCoroutines();
    }
}
