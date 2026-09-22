using VContainer;

namespace Core.Object.Service
{
    public interface IGlobalServiceInstallable
    {
        public void Install(IObjectResolver resolver);
    }
}
