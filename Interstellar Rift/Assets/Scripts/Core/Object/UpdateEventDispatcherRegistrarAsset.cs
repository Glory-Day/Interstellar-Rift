using System;
using Core.Object.Service;
using Core.Utility.Extension;
using UnityEngine;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    [CreateAssetMenu(fileName = "Update Event Dispatcher Factory Asset",
                     menuName = "Assets/Services/Global/Update Event Dispatcher")]
    public class UpdateEventDispatcherRegistrarAsset : GlobalServiceRegistrarAsset
    {
        public override void Register(IContainerBuilder builder)
        {
            Console.LogProgress();

            builder.Register(resolver => new UpdateEventDispatcher(resolver), Lifetime.Scoped)
                   .As<IUpdateEventDispatcher>()
                   .AsSelf();

            Console.LogSuccess($"{nameof(UpdateEventDispatcher).ToNicifyPascalCase().ToBoldStyle()} is registered.");
        }
    }
}
