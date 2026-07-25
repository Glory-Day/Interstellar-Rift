using System;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// An <see cref="IColorApplicator"/> that always applies a single, unchanging color set at construction.
    /// </summary>
    public class FixedColorApplicator : IColorApplicator, IDisposable
    {
        private readonly Color _color;

        /// <param name="color">The fixed color to apply.</param>
        public FixedColorApplicator(Color color)
        {
            _color = color;
        }

        /// <inheritdoc/>
        public void Apply()
        {
            OnColorChanged?.Invoke(_color);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            OnColorChanged = null;
        }

        /// <inheritdoc/>
        public event Action<Color> OnColorChanged;
    }
}
