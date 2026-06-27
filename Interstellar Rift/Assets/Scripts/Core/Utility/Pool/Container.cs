using System;
using Core.Object;
using UnityEngine;
using UnityEngine.Pool;

using Console = GloryDay.Debug.Console;

namespace Core.Utility.Pool
{
    public class Container : IDisposable
    {
        private readonly ObjectPool<GameObject> _pool;

        private readonly GameObject _origin;
        private readonly Transform _parent;

        /// <param name="origin">The original prefab to be cloned by this container.</param>
        /// <param name="parent">The parent transform under which inactive objects are held.</param>
        /// <param name="configuration">The configuration to apply to the object pool.</param>
        public Container(GameObject origin, Transform parent, ObjectPoolConfiguration configuration)
        {
            _origin = origin;
            _parent = parent;

            _pool = new ObjectPool<GameObject>(
                createFunc:      Create_Internal,
                actionOnGet:     Get_Internal,
                actionOnRelease: Release_Internal,
                actionOnDestroy: Destroy_Internal,
                defaultCapacity: configuration.DefaultCapacity,
                maxSize:         configuration.MaximumSize,
                collectionCheck: configuration.IsCollectionChecked);
        }

        /// <summary>
        /// From the pool, select an object and activate it.
        /// </summary>
        public GameObject Get()
        {
            return _pool.Get();
        }

        /// <summary>
        /// Deactivates the specified object and returns it to the pool.
        /// </summary>
        /// <param name="clone">The object to return to the pool.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clone"/> is <see langword="null"/>.</exception>
        public void Release(GameObject clone)
        {
            if (clone == null)
            {
                throw new ArgumentNullException(nameof(clone));
            }

            _pool.Release(clone);
        }

        /// <summary>
        /// Destroys all inactive objects currently held in the pool.
        /// </summary>
        public void Clear()
        {
            _pool.Clear();
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            _pool.Dispose();
        }

        private GameObject Create_Internal()
        {
            Console.LogProgress();

            OnBeforeCreated?.Invoke();

            var clone = UnityEngine.Object.Instantiate(_origin, _parent);
            clone.SetActive(false);

            // Add a releaser component and register a callback for the release method.
            var releaser = clone.AddComponent<ObjectPoolReleaser>();
            releaser.OnRelease += () => Release(clone);

            OnAfterCreated?.Invoke(clone);

            return clone;
        }

        private void Get_Internal(GameObject clone)
        {
            Console.LogProgress();

            OnBeforeGetting?.Invoke(clone);

            clone.SetActive(true);

            OnAfterGetting?.Invoke(clone);
        }

        private void Release_Internal(GameObject clone)
        {
            Console.LogProgress();

            OnBeforeReleased?.Invoke(clone);

            clone.SetActive(false);
            clone.transform.SetParent(_parent, false);

            OnAfterReleased?.Invoke(clone);
        }

        private void Destroy_Internal(GameObject clone)
        {
            Console.LogProgress();

            OnBeforeDestroyed?.Invoke(clone);

            UnityEngine.Object.Destroy(clone);

            OnAfterDestroyed?.Invoke();
        }

        /// <summary>
        /// Occurs before a new object is instantiated in the pool.
        /// </summary>
        public event Action OnBeforeCreated;

        /// <summary>
        /// Occurs after a new object is instantiated in the pool.
        /// </summary>
        public event Action<GameObject> OnAfterCreated;

        /// <summary>
        /// Occurs before an object is retrieved from the pool.
        /// </summary>
        public event Action<GameObject> OnBeforeGetting;

        /// <summary>
        /// Occurs after an object is retrieved from the pool.
        /// </summary>
        public event Action<GameObject> OnAfterGetting;

        /// <summary>
        /// Occurs before an object is returned to the pool.
        /// </summary>
        public event Action<GameObject> OnBeforeReleased;

        /// <summary>
        /// Occurs after an object is returned to the pool.
        /// </summary>
        public event Action<GameObject> OnAfterReleased;

        /// <summary>
        /// Occurs before an object is destroyed due to the pool exceeding its maximum size.
        /// </summary>
        public event Action<GameObject> OnBeforeDestroyed;

        /// <summary>
        /// Occurs after an object is destroyed due to the pool exceeding its maximum size.
        /// </summary>
        public event Action OnAfterDestroyed;

        /// <summary>
        /// Count of deactivated objects.
        /// </summary>
        public int CountDeactivatedObjects => _pool.CountInactive;

        /// <summary>
        /// Count of activated objects.
        /// </summary>
        public int CountActivatedObjects => _pool.CountActive;

        /// <summary>
        /// Count of all objects.
        /// </summary>
        public int CountAllObjects => _pool.CountAll;
    }
}
