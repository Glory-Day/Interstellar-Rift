using VContainer;

namespace Core.Object.Service
{
    public interface IServiceInstaller
    {
        public void Install(IContainerBuilder builder);
    }
}
