using VContainer;

namespace Core.Object.Service
{
    public interface IGlobalServiceRegistrable
    {
        public void Register(IContainerBuilder builder);
    }
}
