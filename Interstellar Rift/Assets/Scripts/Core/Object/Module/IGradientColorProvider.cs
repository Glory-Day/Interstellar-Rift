using UnityEngine;

namespace Core.Object.Module
{
    public interface IGradientColorProvider : IColorProvider
    {
        public Color Evaluate(float time);

        public float Speed { get; set; }
    }
}
