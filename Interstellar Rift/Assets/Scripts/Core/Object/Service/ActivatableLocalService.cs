using Core.Utility.Extension;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Service
{
    public class ActivatableLocalService : LocalService, IActivatable
    {
        private readonly GameObject _gameObject;

        public ActivatableLocalService(GameObject gameObject, ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            _gameObject = gameObject;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _gameObject.SetActive(false);
        }

        public void Enable()
        {
            Console.LogProgress();

            _gameObject.SetActive(true);

            Console.LogMessage($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} has been activated.");
        }

        public void Disable()
        {
            Console.LogProgress();

            _gameObject.SetActive(false);

            Console.LogMessage($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} has been deactivated.");
        }
    }
}
