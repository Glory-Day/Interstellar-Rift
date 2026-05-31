using UnityEngine;

namespace Core.Object.Module
{
    public class GradientColorProvider : IGradientColorProvider
    {
        public GradientColorProvider(Color color, float speed = 1f)
        {
            Color = color;
            Speed = speed;
        }

        public Color Evaluate(float time)
        {
            return Color.HSVToRGB(Mathf.Repeat(time * Speed, 1f), 1f, 1f);
        }

        public Color Color { get; }

        public float Speed { get; set; }
    }
}
