using System;
using UnityEngine;

namespace Core.Object.Module
{
    public class GradientColorApplicator : IColorApplicator, IDisposable
    {
        private readonly UpdateEventHandler _handler;

        private readonly float _speed;

        public GradientColorApplicator(UpdateEventHandler handler, float speed = 1f)
        {
            _handler = handler;
            _speed = speed;
        }

        public void Apply()
        {
            _handler.OnUpdate += Evaluate;
        }

        public void Cancel()
        {
            _handler.OnUpdate -= Evaluate;
        }

        private void Evaluate()
        {
            var color = Color.HSVToRGB(Mathf.Repeat(Time.time * _speed, 1f), 1f, 1f);

            OnColorChanged?.Invoke(color);
        }

        public void Dispose()
        {
            Cancel();

            OnColorChanged = null;
        }

        public event Action<Color> OnColorChanged;
    }
}
