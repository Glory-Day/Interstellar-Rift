using System;
using Core.Object.Service;
using UnityEngine;

namespace Core.Object.Module
{
    public abstract class ColorApplicator : LocalService, IColorApplicator
    {
        protected Color Color;

        public override void Dispose()
        {
            OnColorChanged = null;
        }

        public virtual void Apply()
        {
            OnColorChanged?.Invoke(Color);
        }

        protected void ChangeColor(Color color)
        {
            OnColorChanged?.Invoke(color);
        }

        /// <inheritdoc/>
        public event Action<Color> OnColorChanged;
    }
}
