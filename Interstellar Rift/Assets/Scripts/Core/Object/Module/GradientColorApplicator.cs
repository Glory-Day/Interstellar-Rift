using System;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// An <see cref="IColorApplicator"/> that continuously cycles through the HSV color wheel with every frame.
    /// </summary>
    public class GradientColorApplicator : IColorApplicator, IDisposable
    {
        #region SERVICE FIELD API

        private readonly UpdateEventHandler _updateEventHandler;

        #endregion

        private readonly float _speed;

        /// <param name="updateEventHandler">The <see cref="UpdateEventHandler"/> service to inject.</param>
        /// <param name="speed">How fast the hue cycles through the color wheel per second. Defaults to <c>1</c>.</param>
        public GradientColorApplicator(UpdateEventHandler updateEventHandler, float speed = 1f)
        {
            _updateEventHandler = updateEventHandler;
            _speed = speed;
        }

        /// <summary>
        /// Starts the color cycle updating.
        /// </summary>
        public void Apply()
        {
            _updateEventHandler.OnUpdate += Evaluate;
        }

        /// <summary>
        /// Stops the color cycle updating.
        /// </summary>
        public void Cancel()
        {
            _updateEventHandler.OnUpdate -= Evaluate;
        }

        /// <summary>
        /// Computes the current hue from elapsed time and speed, then the color is applied.
        /// </summary>
        private void Evaluate()
        {
            var color = Color.HSVToRGB(Mathf.Repeat(Time.time * _speed, 1f), 1f, 1f);

            OnColorChanged?.Invoke(color);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Cancel();

            OnColorChanged = null;
        }

        /// <inheritdoc/>
        public event Action<Color> OnColorChanged;
    }
}
