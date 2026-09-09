using VContainer;

namespace Core.Object.Service
{
    public interface IServiceInstallable
    {
        void Install(IContainerBuilder builder);
    }
}
