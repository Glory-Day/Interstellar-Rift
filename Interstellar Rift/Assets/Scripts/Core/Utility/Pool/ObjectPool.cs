using System;
using System.Collections.Generic;
using Core.Utility.Pool.Exception;
using UnityEngine;

using Console = GloryDay.Debug.Console;

namespace Core.Utility.Pool
{
    public class ObjectPool : IDisposable
    {
        private readonly Dictionary<GameObject, Container> _containers = new Dictionary<GameObject, Container>();

        private readonly Transform _root;

        /// <param name="root">
        /// The root transform under which all pool containers are organized.
        /// If <see langword="null"/>, a new root <see cref="GameObject"/> is created automatically.
        /// </param>
        public ObjectPool(Transform root = null)
        {
            _root = root ?? new GameObject("Object Pool").transform;
        }

        /// <summary>
        /// Registers a new pool container for the original prefab specified in the configuration.
        /// If the prefab is already registered, the registration is skipped.
        /// </summary>
        /// <param name="configuration">The configuration to apply to the new pool container.</param>
        /// <exception cref="ArgumentNullException">Thrown when the original prefab in <paramref name="configuration"/> is <see langword="null"/>.</exception>
        public void Register(ObjectPoolConfiguration configuration)
        {
            var origin = configuration.Origin;

            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            if (_containers.ContainsKey(origin))
            {
                Console.LogWarning($"Pool for {origin.name} is already registered.");

                return;
            }

            var container = CreateContainer(configuration);
            _containers.Add(origin, container);

            if (configuration.PrewarmCount > 0)
            {
                Prewarm(configuration.Origin, configuration.PrewarmCount);
            }
        }

        /// <summary>
        /// Unregisters the pool container associated with the specified original prefab and disposes it.
        /// </summary>
        /// <param name="origin">The original prefab whose pool container is to be unregistered.</param>
        /// <exception cref="NotContainedException">Thrown when no container is found for <paramref name="origin"/>.</exception>
        public void Unregister(GameObject origin)
        {
            if (_containers.TryGetValue(origin, out var container) == false)
            {
                throw new NotContainedException(origin.name);
            }

            container.Dispose();
            _containers.Remove(origin);
        }

        /// <summary>
        /// Pre-creates the specified number of objects and immediately returns them to the pool.
        /// </summary>
        /// <param name="origin">The original prefab to prewarm.</param>
        /// <param name="count">The number of objects to pre-create.</param>
        public void Prewarm(GameObject origin, int count)
        {
            var container = GetContainer(origin);
            var clones = new GameObject[count];
            for (var i = 0; i < count; i++)
            {
                clones[i] = container.Get();
                container.Release(clones[i]);
            }
        }

        /// <summary>
        /// Disposes all pool containers and destroys the root <see cref="GameObject"/>.
        /// </summary>
        public void Dispose()
        {
            foreach (var container in _containers.Values)
            {
                container.Dispose();
            }

            _containers.Clear();

            if (_root != null)
            {
                UnityEngine.Object.Destroy(_root.gameObject);
            }
        }

        /// <summary>
        /// Creates a new pool container for the original prefab specified in the configuration.
        /// </summary>
        /// <param name="configuration">The configuration to apply to the new pool container.</param>
        /// <returns>The newly created pool container.</returns>
        private Container CreateContainer(ObjectPoolConfiguration configuration)
        {
            var origin = configuration.Origin;
            var container = new GameObject($"{origin.name} (Pool)");
            container.transform.SetParent(_root);

            var pool = new Container(origin, container.transform, configuration);

            return pool;
        }

        public Container GetContainer(GameObject origin)
        {
            if (_containers.TryGetValue(origin, out var container))
            {
                return container;
            }

            Console.LogWarning($"Pool for {origin.name} was not found. Therefore, a new default pool has been created.");

            container = CreateContainer(new ObjectPoolConfiguration { Origin = origin });
            _containers.Add(origin, container);

            return container;
        }

        /// <summary>
        /// Retrieves an object from the pool associated with the specified original prefab and activates it.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to retrieve an object from.</param>
        /// <returns>The activated object.</returns>
        public GameObject Get(GameObject origin)
        {
            return GetContainer(origin).Get();
        }

        /// <summary>
        /// Retrieves an object from the pool and sets its position and rotation.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to retrieve an object from.</param>
        /// <param name="position">The world position to assign to the retrieved object.</param>
        /// <param name="rotation">The world rotation to assign to the retrieved object.</param>
        /// <returns>The activated object.</returns>
        public GameObject Get(GameObject origin, Vector3 position, Quaternion rotation)
        {
            var clone = Get(origin);
            clone.transform.SetPositionAndRotation(position, rotation);

            return clone;
        }

        /// <summary>
        /// Retrieves an object from the pool and sets its position, rotation, and parent.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to retrieve an object from.</param>
        /// <param name="position">The world position to assign to the retrieved object.</param>
        /// <param name="rotation">The world rotation to assign to the retrieved object.</param>
        /// <param name="parent">The parent transform to assign to the retrieved object.</param>
        /// <returns>The activated object.</returns>
        public GameObject Get(GameObject origin, Vector3 position, Quaternion rotation, Transform parent)
        {
            var clone = Get(origin, position, rotation);
            clone.transform.SetParent(parent, true);

            return clone;
        }

        /// <summary>
        /// Destroys all inactive objects in the pool associated with the specified original prefab.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to clear.</param>
        /// <exception cref="NotContainedException">Thrown when no container is found for <paramref name="origin"/>.</exception>
        public void Clear(GameObject origin)
        {
            if (_containers.TryGetValue(origin, out var container) == false)
            {
                throw new NotContainedException(origin.name);
            }

            container.Clear();
        }

        /// <summary>
        /// Destroys all inactive objects in every registered pool container.
        /// </summary>
        public void ClearAll()
        {
            foreach (var container in _containers.Values)
            {
                container.Clear();
            }
        }

        /// <summary>
        /// Returns the number of activated objects in the pool associated with the specified original prefab.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to query.</param>
        /// <returns>The number of activated objects.</returns>
        /// <exception cref="NotContainedException">Thrown when no container is found for <paramref name="origin"/>.</exception>
        public int CountActivatedObjects(GameObject origin)
        {
            return _containers.TryGetValue(origin, out var container) ? container.CountActivatedObjects :
                throw new NotContainedException(origin.name);
        }

        /// <summary>
        /// Returns the number of deactivated objects in the pool associated with the specified original prefab.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to query.</param>
        /// <returns>The number of deactivated objects.</returns>
        /// <exception cref="NotContainedException">Thrown when no container is found for <paramref name="origin"/>.</exception>
        public int CountDeactivatedObjects(GameObject origin)
        {
            return _containers.TryGetValue(origin, out var container) ? container.CountDeactivatedObjects :
                throw new NotContainedException(origin.name);
        }

        /// <summary>
        /// Returns the total number of objects in the pool associated with the specified original prefab.
        /// </summary>
        /// <param name="origin">The original prefab whose pool to query.</param>
        /// <returns>The total number of objects.</returns>
        /// <exception cref="NotContainedException">Thrown when no container is found for <paramref name="origin"/>.</exception>
        public int CountAllObjects(GameObject origin)
        {
            return _containers.TryGetValue(origin, out var container) ? container.CountAllObjects :
                throw new NotContainedException(origin.name);
        }
    }
}
