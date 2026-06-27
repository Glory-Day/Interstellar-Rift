using System.Collections.Generic;
using GloryDay.Debug;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Core.Object.Module
{
    public class ModuleServiceResolver : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [SerializeField] private List<ModuleServiceBehaviour> services;

        #endregion

        private IObjectResolver _resolver;

        [Inject]
        private void Install(IObjectResolver resolver)
        {
            Console.LogProgress();

            _resolver = resolver;
        }

        public TService GetLocalService<TService>() where TService : ModuleServiceBehaviour
        {
            Console.LogProgress();

            var count = services.Count;
            for (var i = 0; i < count; i++)
            {
                if (services[i] is TService)
                {
                    return (TService)services[i];
                }
            }

            return null;
        }

        public TService GetGlobalService<TService>()
        {
            Console.LogProgress();

            return _resolver.Resolve<TService>();
        }
    }
}
