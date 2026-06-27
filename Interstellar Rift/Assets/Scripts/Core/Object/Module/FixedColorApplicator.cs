using System;
using UnityEngine;

namespace Core.Object.Module
{
    public class FixedColorApplicator : IColorApplicator, IDisposable
    {
        private readonly Color _color;

        public FixedColorApplicator(Color color)
        {
            _color = color;
        }

        public void Apply()
        {
            OnColorChanged?.Invoke(_color);
        }

        public void Dispose()
        {
            OnColorChanged = null;
        }

        public event Action<Color> OnColorChanged;
    }
}
