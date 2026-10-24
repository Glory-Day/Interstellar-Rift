using System;
using Core.Object.Service;
using Core.Utility.Attributes;
using UnityEngine;
using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    /// <summary>
    /// An <see cref="IColorApplicator"/> that always applies a single, unchanging color set at construction.
    /// </summary>
    [ExcludeFromInstallation]
    public class FixedColorApplicator : ColorApplicator
    {
        /// <param name="color">The fixed color to apply.</param>
        public FixedColorApplicator(Color color)
        {
            Console.LogProgress();

            Color = color;
        }
    }
}
