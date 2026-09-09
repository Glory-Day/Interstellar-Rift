using System;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Represents an object that applies a color and notifies listeners when that color changes.
    /// </summary>
    public interface IColorApplicator
    {
        /// <summary>
        /// Applies the current color.
        /// </summary>
        public void Apply();

        /// <summary>
        /// Occurs whenever the color that should be applied changes, passing the new color.
        /// </summary>
        public event Action<Color> OnColorChanged;
    }
}
