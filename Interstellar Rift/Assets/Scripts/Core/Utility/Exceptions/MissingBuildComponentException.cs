using System;
using Core.Utility.Extensions;

namespace Core.Utility.Exceptions
{
    /// <summary>
    /// Thrown when a builder's Build() method is invoked while a required component
    /// has not yet been supplied, indicating the resulting object would be in an invalid state.
    /// </summary>
    public sealed class MissingBuildComponentException : InvalidOperationException
    {
        public MissingBuildComponentException(Type targetType, string missingComponentName) : base(CreateMessage(targetType, missingComponentName))
        {
            TargetType = targetType;

            MissingComponentName = missingComponentName;
        }

        private static string CreateMessage(Type targetType, string missingComponentName)
        {
            var targetTypeName = targetType.Name.ToNicifyPascalCase();

#if UNITY_EDITOR

            targetTypeName = targetTypeName.ToBoldStyle();
            missingComponentName = missingComponentName.ToBoldStyle();

#endif

            return $"Cannot build \"{targetTypeName}\" because the required component \"{missingComponentName}\" is missing.";
        }

        public Type TargetType { get; }

        public string MissingComponentName { get; }
    }
}
