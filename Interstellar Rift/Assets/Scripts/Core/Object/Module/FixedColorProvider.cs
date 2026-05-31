using UnityEngine;

namespace Core.Object.Module
{
    public class FixedColorProvider : IColorProvider
    {
        public FixedColorProvider(Color color)
        {
            Color = color;
        }

        public Color Color { get; private set; }
    }
}
