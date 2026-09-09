using Core.Object.Service;
using System;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    public class UpdateEventDispatcher : GlobalService, IUpdateEventDispatcher
    {
        public UpdateEventDispatcher(IObjectResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

            OnUpdate = null;
            OnFixedUpdate = null;
            OnLateUpdate = null;
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        public void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        public event Action OnUpdate;

        public event Action OnFixedUpdate;

        public event Action OnLateUpdate;
    }
}
