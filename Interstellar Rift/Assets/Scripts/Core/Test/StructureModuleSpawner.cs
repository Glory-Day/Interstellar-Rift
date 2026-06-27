#if UNITY_EDITOR

using System;
using Core.Object.Module;
using Core.Object.Module.Structure;
using Core.Utility.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

using Console = GloryDay.Debug.Console;

namespace Core.Test
{
    public class StructureModuleSpawner : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [SerializeField] private ModuleTestbed[] testbeds;

        #endregion

        private IObjectResolver _resolver;

        private ObjectPool _objectPool;

        [Inject]
        private void Install(IObjectResolver resolver)
        {
            Console.LogProgress();

            _resolver = resolver;

            _objectPool = _resolver.Resolve<ObjectPool>();
        }

        private void Start()
        {
            Console.LogProgress();

            Spawn();
        }

        private void Spawn()
        {
            Console.LogProgress();

            var length = testbeds.Length;
            for (var i = 0; i < length; i++)
            {
                var rank = testbeds[i].rank;
                var module = testbeds[i].module;
                var database = testbeds[i].database;
                var spawner = testbeds[i].spawner;

                var configuration = new ObjectPoolConfiguration
                {
                    Origin = module,
                    DefaultCapacity = 5,
                    MaximumSize = 10,
                    PrewarmCount = 0,
                    IsCollectionChecked = true
                };

                _objectPool.Register(configuration);

                var container = _objectPool.GetContainer(module);
                container.OnAfterCreated += InstallServices;
                container.OnAfterCreated += clone => BootModule(clone, rank, database);

                var clone = _objectPool.Get(module, spawner.position, spawner.rotation);

                Console.LogSuccess($"{clone.name} is successfully spawned.");
            }
        }

        private void InstallServices(GameObject clone)
        {
            Console.LogProgress();

            _resolver.InjectGameObject(clone);

            Console.LogEventMessage("All services are installed.");
        }

        private void BootModule(GameObject clone, ModuleRank rank, ModuleDataTable database)
        {
            Console.LogProgress();

            var label = clone.name[..^14];
            var resolver = clone.GetComponent<ModuleServiceResolver>();
            var model = new StructureModuleModelFactory(label, rank, database).Create();
            var bootstrap = new StructureModuleBootstrapFactory(model, resolver).Create();
            bootstrap.Boot();

            Console.LogEventMessage("Module is booting completely.");
        }

        #region SERIALIZABLE STRUCTURE API

        [Serializable]
        private struct ModuleTestbed
        {
            public ModuleRank rank;
            public GameObject module;
            public ModuleDataTable database;
            public Transform spawner;
        }

        #endregion
    }
}

#endif
