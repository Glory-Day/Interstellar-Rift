using System.Collections.Generic;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Editor-only. Holds information used for visual debugging in the Unity Editor.
    /// </summary>
    public class SearchedModuleGizmosInformation
    {
        /// <summary>
        /// The position of this object.
        /// </summary>
        public Vector3 MainPosition { get; set; }

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
}
