using Core.Object.Service;
using Core.Utility;
using Core.Utility.Extension;
using UnityEngine;
using VContainer;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Service
{
    [CreateAssetMenu(fileName = "Local Service Repository Registrar Asset",
                     menuName = "Assets/Services/Global/Local Service Repository")]
    public class LocalServiceRepositoryRegistrarAsset : GlobalServiceRegistrarAsset
    {
        public override void Register(IContainerBuilder builder)
        {
            Console.LogProgress();

            builder.Register(resolver => new LocalServiceRepository(resolver), Lifetime.Scoped)
                   .As<IRegistrable<Collider2D, ILocalServiceResolver>>()
                   .AsSelf();

            Console.LogSuccess($"{nameof(LocalServiceRepository).ToNicifyPascalCase().ToBoldStyle()} is registered.");
        }
    }
}
