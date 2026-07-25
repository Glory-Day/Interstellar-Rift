using System.Collections.Generic;
using Core.Object.Service;
using Core.Utility.Pool;
using GloryDay.Debug;
using GloryDay.Debug.Gizmos;
using Sirenix.OdinInspector;
using UnityEngine;

using ColorUtility = UnityEngine.ColorUtility;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the ability to search for the nearest module the dragged module can attach to, and the nearest attachable slot on that module.
    /// </summary>
    public class ModuleSearcher : LocalServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Configuration")]
        [Tooltip("Radius for searching modules.")]
        [SerializeField] private float radius;
        [Tooltip("The layer mask of modules to search for.")]
        [SerializeField] private LayerMask target;

        #endregion

        private readonly List<Collider2D> _buffer = new List<Collider2D>();
        private ContactFilter2D _filter = new ContactFilter2D();

#if UNITY_EDITOR

        private readonly DebugInformation _debug = new DebugInformation();

#endif

        /// <inheritdoc/>
        public override void Initialize()
        {
            Console.LogProgress();

            _filter.useLayerMask = true;
            _filter.layerMask = target;

            base.Initialize();
        }

        /// <summary>
        /// Searches for the nearest attachable slot within the specified radius.
        /// </summary>
        /// <returns>A <see cref="SearchedSlotResult"/> containing the nearest connector and slot, or <see langword="null"/> if no valid slot is found.</returns>
        public SearchedSlotResult? Search()
        {
#if UNITY_EDITOR

            _debug.Origin = transform.position;
            _debug.PositionForModule = null;
            _debug.PositionsForSlot.Clear();
            _debug.ColorsForSlot.Clear();

#endif

            var x = transform.position.x;
            var y = transform.position.y;
            var origin = new Vector2(x, y);
            var count = Physics2D.OverlapCircle(origin, radius, _filter, _buffer);

            Collider2D buffer = null;
            var cache = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                // Excludes self from search results.
                if (_buffer[i].transform == transform.parent)
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

            _debug.PositionForModule = buffer.transform.position;
            _debug.ColorForModule = Color.magenta;

#endif

            // Get the slots attached to the found module.
            var connector = buffer.GetComponentInChildren<ModuleConnector>();
            var slots = connector.Slots;

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
                if (distance > radius)
                {
                    continue;
                }

#if UNITY_EDITOR

                _debug.PositionsForSlot.Add(position);
                _debug.ColorsForSlot.Add(slots[i].State == SlotState.Attachable ? Color.yellow : Color.red);

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

            count = _debug.PositionsForSlot.Count;
            for (var i = 0; i < count; i++)
            {
                if (_debug.PositionsForSlot[i] != slot.transform.position)
                {
                    continue;
                }

                _debug.PositionsForSlot[i] = slot.transform.position;
                _debug.ColorsForSlot[i] = Color.green;
            }

#endif

            return new SearchedSlotResult(radius, connector, slot);
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            // Draws the search radius boundary.
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, radius);

            // Draws a line toward the found module.
            var color = _debug.ColorForModule;
            var origin = _debug.Origin;
            var position = _debug.PositionForModule;
            if (position != null)
            {
                Gizmos.color = color;
                Gizmos.DrawLine(origin, position.Value);
            }

            // Draws lines toward each slot with its attachment state label.
            var style = new GUIStyle { richText = true };
            var builder = new LabelBuilder();
            var count = _debug.PositionsForSlot.Count;
            for (var i = 0; i < count; i++)
            {
                color = _debug.ColorsForSlot[i];
                position = _debug.PositionsForSlot[i];
                Gizmos.color = color;
                Gizmos.DrawLine(origin, position.Value);

                builder.Color = $"#{ColorUtility.ToHtmlStringRGB(color)}";
                builder.FontSize = 12;

                if (color == Color.red)
                {
                    builder.Append("Unavailable");
                }
                else if (color == Color.yellow)
                {
                    builder.Append("Available");
                }
                else
                {
                    builder.Append("Connected");
                }

                UnityEditor.Handles.Label(position.Value, builder.ToString(), style);

                builder.Clear();
            }
        }

#endif

#if UNITY_EDITOR

        #region UNITY EDITOR DEBUG API

        /// <summary>
        /// Editor-only. Holds information used for visual debugging in the Unity Editor.
        /// </summary>
        private class DebugInformation
        {
            /// <summary>
            /// The position of this object.
            /// </summary>
            public Vector3 Origin { get; set; }

            /// <summary>
            /// The position of the found module GameObject.
            /// </summary>
            public Vector3? PositionForModule { get; set; }

            /// <summary>
            /// The color used to indicate the found module.
            /// </summary>
            public Color ColorForModule { get; set; }

            /// <summary>
            /// The positions of the slots attached to the found module.
            /// </summary>
            public List<Vector3> PositionsForSlot { get; } = new List<Vector3>();

            /// <summary>
            /// The colors used to indicate the attachment state of each slot.
            /// </summary>
            public List<Color> ColorsForSlot { get; } = new List<Color>();
        }

        #endregion

#endif
    }
}
