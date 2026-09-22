using System.Collections.Generic;
using Core.Utility;
using GloryDay.Debug;
using UnityEngine;
using VContainer;

namespace Core.Object.Service
{
    public class LocalServiceRepository : GlobalService, IRegistrable<Collider2D, ILocalServiceResolver>
    {
        private Dictionary<Collider2D, ILocalServiceResolver> _registry = new Dictionary<Collider2D, ILocalServiceResolver>();

        public LocalServiceRepository(IObjectResolver resolver) : base(resolver) { }

        public override void Dispose()
        {
            Console.LogProgress();

            _registry.Clear();
            _registry = null;
        }

        public void Register(Collider2D collider, ILocalServiceResolver socket)
        {
            Console.LogProgress();

            _registry.Add(collider, socket);
        }

        public void UnRegister(Collider2D key)
        {
            Console.LogProgress();

            _registry.Remove(key);
        }

        public bool ContainsKey(Collider2D key)
        {
            return _registry.ContainsKey(key);
        }

        public ILocalServiceResolver Fetch(Collider2D key)
        {
            return _registry[key];
        }

        public bool TryFetch(Collider2D key, out ILocalServiceResolver value)
        {
            return _registry.TryGetValue(key, out value);
        }
    }
}
