using VContainer;

namespace Core.Object.Service
{
    public abstract class GlobalClientBehaviour : ClientBehaviour
    {
        [Inject]
        protected abstract void Install(IObjectResolver resolver);
    }
}
