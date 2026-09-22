using System;
using Core.Utility.Extension;

namespace Core.Utility.Exception
{
    /// <summary>
    /// Thrown when a builder's Build() method is invoked while a required component
    /// has not yet been supplied, indicating the resulting object would be in an invalid state.
    /// </summary>
    public sealed class MissingRequiredComponentException : System.Exception
    {
        /// <inheritdoc/>
        public MissingRequiredComponentException(Type type, string componentName)
            : base($"Required component '{componentName.ToBoldStyle().ToNicifyPascalCase()}' was not provided before {type.Name.ToBoldStyle().ToNicifyPascalCase()}")
        {
            ComponentName = componentName;
        }

        /// <inheritdoc/>
        public MissingRequiredComponentException(Type type, string componentName, string message)
            : base(message)
        {
            ComponentName = componentName;
        }

        /// <inheritdoc/>
        public MissingRequiredComponentException(Type type, string componentName, string message, System.Exception innerException)
            : base(message, innerException)
        {
            ComponentName = componentName;
        }

        /// <summary>
        /// The name of the required component that was missing at build time.
        /// </summary>
        public string ComponentName { get; }
    }
}
