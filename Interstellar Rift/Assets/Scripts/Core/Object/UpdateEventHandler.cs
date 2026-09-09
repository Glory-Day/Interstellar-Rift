using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;
using VContainer;

namespace Core.Object
{
    /// <summary>
    /// A global service that exposes Unity's <c>Update</c>, <c>FixedUpdate</c>, and <c>LateUpdate</c> messages as events,
    /// allowing non-<see cref="MonoBehaviour"/> classes to subscribe to per-frame updates.
    /// </summary>
    public class UpdateEventHandler : GlobalClientBehaviour
    {
        private UpdateEventDispatcher _updateEventDispatcher;

        [Inject]
        protected override void Install(IObjectResolver resolver)
        {
            Console.LogProgress();

            _updateEventDispatcher = resolver.Resolve<UpdateEventDispatcher>();
        }

        private void Update()
        {
            _updateEventDispatcher.Update();
        }

        private void FixedUpdate()
        {
            _updateEventDispatcher.FixedUpdate();
        }

        private void LateUpdate()
        {
            _updateEventDispatcher.LateUpdate();
        }
    }
}
