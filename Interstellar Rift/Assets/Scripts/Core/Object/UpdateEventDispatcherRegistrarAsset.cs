using System;
using Core.Object.Service;
using Core.Utility.Extensions;
using UnityEngine;
using VContainer;
using Console = GloryDay.Debug.Console;

namespace Core.Object
{
    [CreateAssetMenu(fileName = "Update Event Dispatcher Registrar Asset",
                     menuName = "Assets/Services/Global/Update Event Dispatcher")]
    public class UpdateEventDispatcherRegistrarAsset : GlobalServiceRegistrarAsset
    {
        public override void Register(IContainerBuilder builder)
        {
            Console.LogProgress();

            builder.RegisterInstance(new UpdateEventDispatcher())
                   .As<IUpdateEventDispatcher>()
                   .AsSelf();

            Console.LogSuccess($"{nameof(UpdateEventDispatcher).ToNicifyPascalCase().ToBoldStyle()} is registered.");
        }
    }
}
