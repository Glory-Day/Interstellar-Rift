namespace Core.Object.Service
{
    /// <summary>
    /// Represents a service that is accessible to every client object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A global service does not belong to any module and is resolved through VContainer.
    /// Apart from the range of client objects to which it is accessible, it follows the same
    /// lifetime and disposal contract as every other <see cref="Service"/>.
    /// </para>
    /// <para>
    /// A global service may depend on other global services to carry out its logic. Derived
    /// classes declare such dependencies as constructor parameters, which are supplied when
    /// the service is resolved. A global service must not depend on a local service, since
    /// a local service is accessible only within its own module.
    /// </para>
    /// </remarks>
    public abstract class GlobalService : Service { }
}
