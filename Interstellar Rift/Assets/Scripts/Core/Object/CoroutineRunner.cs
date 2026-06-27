using System.Collections;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void OnDestroy()
        {
            Console.LogProgress();

            StopAllCoroutines();
        }

        void ICoroutineRunner.StartCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        void ICoroutineRunner.StopCoroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }

        void ICoroutineRunner.StopAllCoroutines()
        {
            StopAllCoroutines();
        }
    }
}
