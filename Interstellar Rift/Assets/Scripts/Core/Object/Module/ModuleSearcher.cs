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
        #region GLOBAL SERVICE API

        private LocalServiceRepository _localServiceRepository;

        #endregion

        #region LOCAL SERVICE API

        private ModuleSocket _moduleSocket;

        private Transform _transform;

        #endregion

        private readonly float _range;

        private List<Collider2D> _buffer = new List<Collider2D>();
        private readonly ContactFilter2D _filter = new ContactFilter2D();

        public ModuleSearcher(float range, LayerMask target,
                              LocalServiceRepository localServiceRepository,
                              ModuleSocket moduleSocket,
                              Transform transform)
        {
            Console.LogProgress();

            _localServiceRepository = localServiceRepository;

            _moduleSocket = moduleSocket;

            _transform = transform;

            _range = range;

            _filter.useLayerMask = true;
            _filter.layerMask = target;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _localServiceRepository = null;

            _moduleSocket = null;

            _transform = null;

            _buffer.Clear();
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

            var selected = -1;
            var position = _transform.position;

#endif

            var origin = _transform.position;
            var count = Physics2D.OverlapCircle(origin, _range, _filter, _buffer);

            ModuleSocket socket = null;
            Slot slot = null;
            var nearest = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                var cache = _localServiceRepository.Fetch(_buffer[i]).GetLocalService<ModuleSocket>();

                // Excludes this module and its own subtree.
                if (cache == null || _moduleSocket.IsAncestorOf(cache))
                {
                    continue;
                }

                var slots = cache.Slots;
                var length = slots.Count;

                for (var j = 0; j < length; j++)
                {
                    var distance = Vector2.Distance(origin, slots[j].transform.position);
                    if (distance > _range)
                    {
                        continue;
                    }

#if UNITY_EDITOR

                    CachedGizmosInformation.PositionsForSlot.Add(position);
                    CachedGizmosInformation.ColorsForSlot.Add(slots[j].State == SlotState.Attachable ? Color.yellow : Color.red);

#endif

                    // Excludes slots that cannot be connected.
                    if (slots[j].State == SlotState.Unattachable)
                    {
                        continue;
                    }

                    if (distance >= nearest)
                    {
                        continue;
                    }

                    socket = cache;
                    slot = slots[j];
                    nearest = distance;

#if UNITY_EDITOR

                    selected = CachedGizmosInformation.PositionsForSlot.Count - 1;
                    position = _buffer[i].transform.position;

#endif
                }
            }

            // Returns null if no attachable slot is found.
            if (slot == null)
            {
                return null;
            }

#if UNITY_EDITOR

            CachedGizmosInformation.PositionForModule = position;
            CachedGizmosInformation.ColorForModule = Color.magenta;
            CachedGizmosInformation.ColorsForSlot[selected] = Color.green;

#endif

            return new SearchedSlotResult(socket, slot);
        }

        public float Range => _range;

#if UNITY_EDITOR

        public SearchedModuleGizmosInformation CachedGizmosInformation { get; } = new SearchedModuleGizmosInformation();

#endif
    }
}
