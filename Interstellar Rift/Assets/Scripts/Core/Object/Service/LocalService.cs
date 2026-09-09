using GloryDay.Debug;

namespace Core.Object.Service
{
    public abstract class LocalService : Service
    {
        protected LocalService(ServiceResolver resolver)
        {
            Resolver = resolver;
        }

        protected ServiceResolver Resolver { get; }
    }
}
