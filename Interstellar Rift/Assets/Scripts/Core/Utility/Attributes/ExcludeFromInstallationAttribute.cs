using System;
using Core.Utility.Extensions;

namespace Core.Utility.Attributes
{
    /// <summary>
    /// Indicates that the marked class is not used as a key when a service is installed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A service is installed under each type in its class hierarchy, so that it can be resolved
    /// not only by its own type but also by its base types. This attribute is used to select which
    /// classes in the hierarchy serve as keys: applying it to a class that is not needed as a key
    /// excludes that class, so that only the remaining classes are used as keys. The types are
    /// enumerated by
    /// <see cref="TypeExtensions.GetHierarchyWithoutExcluded{TAttribute}(Type)"/>.
    /// </para>
    /// <para>
    /// This attribute is not inherited. It excludes only the class to which it is directly applied,
    /// and does not affect the classes derived from it.
    /// </para>
    /// <example>
    /// For example, applying this attribute to a derived class whose own functionality is not needed
    /// by its clients causes the service to be resolved only through its base type. The clients then
    /// depend only on the base type, so that the derived class can be replaced without changing them.
    /// </example>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ExcludeFromInstallationAttribute : Attribute { }
}
