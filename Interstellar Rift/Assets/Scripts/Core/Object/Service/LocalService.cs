using Core.Utility.Attributes;

namespace Core.Object.Service
{
    /// <summary>
    /// Represents a service that is accessible only to the client objects within a single module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A local service belongs to a single module and is resolved through the
    /// <see cref="ServiceResolver"/> of that module. Apart from the range of client objects
    /// to which it is accessible, it follows the same lifetime and disposal contract as
    /// every other <see cref="Service"/>.
    /// </para>
    /// <para>
    /// A local service may depend on other services, both global and local, to carry out
    /// its logic. Derived classes declare such dependencies as constructor parameters,
    /// which are obtained through the <see cref="ServiceResolver"/> of the module and
    /// supplied when the service is created.
    /// </para>
    /// </remarks>
    [ExcludeFromInstallation]
    public abstract class LocalService : Service { }
}
