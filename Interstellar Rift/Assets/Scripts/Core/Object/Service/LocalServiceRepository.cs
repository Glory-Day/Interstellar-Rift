using System.Collections.Generic;
using Core.Utility;
using GloryDay.Debug;
using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    /// <summary>
    /// A repository for registering and querying local services associated with key.
    /// </summary>
    public class LocalServiceRepository
        : GlobalService,
            IRegistry<Collider2D, ILocalServiceResolver>
    {
        private Dictionary<Collider2D, ILocalServiceResolver> _registry =
            new Dictionary<Collider2D, ILocalServiceResolver>();

        /// <param name="resolver">The provider of global services installed via VContainer.</param>
        public LocalServiceRepository() { }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Console.LogProgress();

            _registry.Clear();
            _registry = null;
        }

        /// <inheritdoc/>
        public void Register(Collider2D key, ILocalServiceResolver value)
        {
            Console.LogProgress();

            _registry.Add(key, value);
        }

        /// <inheritdoc/>
        public void Unregister(Collider2D key)
        {
            Console.LogProgress();

            _registry.Remove(key);
        }

        /// <inheritdoc/>
        public bool ContainsKey(Collider2D key)
        {
            return _registry.ContainsKey(key);
        }

        /// <inheritdoc/>
        public ILocalServiceResolver Fetch(Collider2D key)
        {
            return _registry[key];
        }

        /// <inheritdoc/>
        public bool TryFetch(Collider2D key, out ILocalServiceResolver value)
        {
            return _registry.TryGetValue(key, out value);
        }
    }
}
