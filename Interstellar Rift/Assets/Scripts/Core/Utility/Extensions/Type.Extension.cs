using System;
using System.Collections.Generic;

namespace Core.Utility.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Enumerates the specified type and its base types, excluding the types to which
        /// <typeparamref name="TAttribute"/> is directly applied.
        /// </summary>
        /// <param name="type">The type from which the enumeration starts.</param>
        /// <typeparam name="TAttribute">The type of the attribute that marks the types to exclude.</typeparam>
        /// <returns>
        /// The types in the class hierarchy of <paramref name="type"/>, ordered from the most derived type
        /// to the least derived type, excluding <see cref="object"/> and the types to which
        /// <typeparamref name="TAttribute"/> is directly applied.
        /// </returns>
        /// <remarks>
        /// Only the attributes applied directly to each type are checked; an attribute applied to a base type
        /// does not exclude the types derived from it. Interfaces implemented by the types are not enumerated.
        /// The enumeration is deferred until the result is iterated.
        /// </remarks>
        /// <example>
        /// For example, this method is used with <see cref="Core.Utility.Attributes.ExcludeFromInstallationAttribute"/>
        /// to determine the types under which a service is installed.
        /// </example>
        public static IEnumerable<Type> GetHierarchyWithoutExcluded<TAttribute>(this Type type) where TAttribute : Attribute
        {
            for (; type != null && type != typeof(object); type = type.BaseType)
            {
                if (type.IsDefined(typeof(TAttribute), false)) continue;
                yield return type;
            }
        }
    }
}
