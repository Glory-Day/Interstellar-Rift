using System;
using Core.Object.Service;
using UnityEngine;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// An <see cref="IColorApplicator"/> that always applies a single, unchanging color set at construction.
    /// </summary>
    public class FixedColorApplicator : ColorApplicator
    {
        /// <param name="color">The fixed color to apply.</param>
        public FixedColorApplicator(Color color, ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            Color = color;
        }
    }
}
