using System;
using Core.Object.Service;
using UnityEngine;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    /// <summary>
    /// A global service that exposes Unity's <c>Update</c>, <c>FixedUpdate</c>, and <c>LateUpdate</c> messages as events,
    /// allowing non-<see cref="MonoBehaviour"/> classes to subscribe to per-frame updates.
    /// </summary>
    public class UpdateEventHandler : GlobalServiceInstallationBehaviour
    {
        private UpdateEventDispatcher _updateEventDispatcher;

        [Inject]
        public override void Install(IObjectResolver resolver)
        {
            Console.LogProgress();

            _updateEventDispatcher = resolver.Resolve<UpdateEventDispatcher>();

            base.Install(resolver);
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

        private void OnDestroy()
        {
            Console.LogProgress();

            _updateEventDispatcher = null;
        }
    }
}
