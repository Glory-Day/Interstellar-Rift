using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleServiceBehaviour : MonoBehaviour, IModuleService
    {
        public virtual void Enable()
        {
            enabled = true;
        }

        public virtual void Disable()
        {
            enabled = false;
        }
    }
}
