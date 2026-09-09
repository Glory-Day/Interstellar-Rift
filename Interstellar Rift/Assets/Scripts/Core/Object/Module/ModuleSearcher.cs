using System.Collections.Generic;
using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the ability to search for the nearest module the dragged module can attach to, and the nearest attachable slot on that module.
    /// </summary>
    public class ModuleSearcher : LocalService
    {
        #region LOCAL SERVICE API

        private Transform _transform;

        #endregion

        private readonly float _radius;

        private List<Collider2D> _buffer = new List<Collider2D>();
        private readonly ContactFilter2D _filter = new ContactFilter2D();

        public ModuleSearcher(float radius, LayerMask target, ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            _transform = resolver.GetLocalService<ModuleTransformResolver>().Main;

            _radius = radius;

            _filter.useLayerMask = true;
            _filter.layerMask = target;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _buffer.Clear();

            _transform = null;

            _buffer = null;
        }

        /// <summary>
        /// Searches for the nearest attachable slot within the specified radius.
        /// </summary>
        /// <returns>A <see cref="SearchedSlotResult"/> containing the nearest connector and slot, or <see langword="null"/> if no valid slot is found.</returns>
        public SearchedSlotResult? Search()
        {
#if UNITY_EDITOR

            CachedGizmosInformation.MainPosition = _transform.position;
            CachedGizmosInformation.PositionForModule = null;
            CachedGizmosInformation.PositionsForSlot.Clear();
            CachedGizmosInformation.ColorsForSlot.Clear();

#endif

            var x = _transform.position.x;
            var y = _transform.position.y;
            var origin = new Vector2(x, y);
            var count = Physics2D.OverlapCircle(origin, _radius, _filter, _buffer);

            Collider2D buffer = null;
            var cache = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                // Excludes self from search results.
                if (_buffer[i].transform == _transform.parent)
                {
                    continue;
                }

                x = _buffer[i].transform.position.x;
                y = _buffer[i].transform.position.y;
                var position = new Vector2(x, y);
                var distance = Vector2.Distance(origin, position);

                if (distance < cache)
                {
                    buffer = _buffer[i];
                    cache = distance;
                }
            }

            // Returns null if no module is found.
            if (buffer == null)
            {
                return null;
            }

#if UNITY_EDITOR

            CachedGizmosInformation.PositionForModule = buffer.transform.position;
            CachedGizmosInformation.ColorForModule = Color.magenta;

#endif

            // Get the slots attached to the found module.
            var socket = buffer.GetComponentInChildren<ModuleSocket>();
            var slots = socket.Slots;

            Slot slot = null;
            cache = float.MaxValue;
            count = slots.Count;

            for (var i = 0; i < count; i++)
            {
                x = slots[i].transform.position.x;
                y = slots[i].transform.position.y;
                var position = new Vector2(x, y);
                var distance = Vector2.Distance(origin, position);

                // Excludes slots beyond the search radius.
                if (distance > _radius)
                {
                    continue;
                }

#if UNITY_EDITOR

                CachedGizmosInformation.PositionsForSlot.Add(position);
                CachedGizmosInformation.ColorsForSlot.Add(slots[i].State == SlotState.Attachable ? Color.yellow : Color.red);

#endif

                // Excludes slots that cannot be connected.
                if (slots[i].State == SlotState.Unattachable)
                {
                    continue;
                }

                if (distance < cache)
                {
                    slot = slots[i];
                    cache = distance;
                }
            }

            // Returns null if no nearest slot is found.
            if (slot == null)
            {
                return null;
            }

#if UNITY_EDITOR

            count = CachedGizmosInformation.PositionsForSlot.Count;
            for (var i = 0; i < count; i++)
            {
                if (CachedGizmosInformation.PositionsForSlot[i] != slot.transform.position)
                {
                    continue;
                }

                CachedGizmosInformation.PositionsForSlot[i] = slot.transform.position;
                CachedGizmosInformation.ColorsForSlot[i] = Color.green;
            }

#endif

            return new SearchedSlotResult(socket, slot);
        }

        public float Radius => _radius;

#if UNITY_EDITOR

        public SearchedModuleGizmosInformation CachedGizmosInformation { get; } = new SearchedModuleGizmosInformation();

#endif
    }
}
