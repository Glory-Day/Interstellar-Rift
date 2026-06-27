using System.Collections;
using UnityEngine;

namespace Core.Object
{
    public interface ICoroutineRunner
    {
        public void StartCoroutine(IEnumerator routine);

        public void StopCoroutine(Coroutine routine);

        public void StopAllCoroutines();
    }
}
