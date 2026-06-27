using System;
using UnityEngine;

namespace Core.Object.Module
{
    public interface IColorApplicator
    {
        public void Apply();

        public event Action<Color> OnColorChanged;
    }
}
