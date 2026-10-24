using System;
using Core.Object.Service;
using Core.Utility.Attributes;
using UnityEngine;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// An <see cref="IColorApplicator"/> that continuously cycles through the HSV color wheel with every frame.
    /// </summary>
    [ExcludeFromInstallation]
    public class GradientColorApplicator : ColorApplicator
    {
        #region GLOBAL SERVICE API

        private UpdateEventDispatcher _updateEventDispatcher;

        #endregion

        private readonly float _speed;

        /// <param name="speed">How fast the hue cycles through the color wheel per second. Defaults to <c>1</c>.</param>
        public GradientColorApplicator(float speed, UpdateEventDispatcher updateEventDispatcher)
        {
            Console.LogProgress();

            _speed = speed;

            _updateEventDispatcher = updateEventDispatcher;
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public override void Dispose()
        {
            Console.LogProgress();

            Cancel();

            base.Dispose();
        }

        /// <summary>
        /// Starts the color cycle updating.
        /// </summary>
        public override void Apply()
        {
            _updateEventDispatcher.OnUpdate += Evaluate;
        }

        /// <summary>
        /// Stops the color cycle updating.
        /// </summary>
        public void Cancel()
        {
            _updateEventDispatcher.OnUpdate -= Evaluate;
        }

        /// <summary>
        /// Computes the current hue from elapsed time and speed, then the color is applied.
        /// </summary>
        private void Evaluate()
        {
            Color = Color.HSVToRGB(Mathf.Repeat(Time.time * _speed, 1f), 1f, 1f);

            base.Apply();
        }
    }
}
