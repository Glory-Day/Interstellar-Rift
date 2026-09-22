#if UNITY_EDITOR

using Core.Object.Module;
using Core.Object.Module.Structure;
using Core.Utility.Extension;
using Core.Utility.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

using Console = GloryDay.Debug.Console;

namespace Core.Test
{
    public class StructureModuleSpawner : MonoBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("References")]
        [Title("Module Testbed Table", HorizontalLine = false)]
        [SerializeField] private ModuleSpawnSpecification[] testbeds;
        [Title("Object Pool", HorizontalLine = false)]
        [SerializeField] private ObjectPoolAsset[] assets;

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

            PoolModuleTestbeds();
            PoolAssets();

            Spawn();
        }

        private void PoolAssets()
        {
            Console.LogProgress();

            var length = assets.Length;
            for (var i = 0; i < length; i++)
            {
                var asset = assets[i].asset;
                var count = assets[i].count;

                var configuration = new Configuration
                {
                    Origin = asset,
                    DefaultCapacity = count,
                    MaximumSize = count,
                    PrewarmCount = 0,
                    IsCollectionChecked = true
                };

                _objectPool.Register(configuration);
                _objectPool.GetContainer(asset);

                Console.LogSuccess($"{asset.name.ToBoldStyle()} is successfully spawned.");
            }
        }

        private void PoolModuleTestbeds()
        {
            Console.LogProgress();

            foreach (var testbed in testbeds)
            {
                var module = testbed.prefab;

                var configuration = new Configuration
                {
                    Origin = module,
                    DefaultCapacity = 5,
                    MaximumSize = 10,
                    PrewarmCount = 0,
                    IsCollectionChecked = true
                };

                _objectPool.Register(configuration);

                var container = _objectPool.GetContainer(module);
                container.OnAfterCreated += clone => BootStructureModule(clone, testbed);

                Console.LogSuccess($"{module.name.ToBoldStyle()} is successfully spawned.");
            }
        }

        private void Spawn()
        {
            Console.LogProgress();

            var length = testbeds.Length;
            for (var i = 0; i < length; i++)
            {
                var module = testbeds[i].prefab;
                var spawner = testbeds[i].spawner;

                var clone = _objectPool.Get(module, spawner.position, spawner.rotation);

                Console.LogSuccess($"{clone.name.ToBoldStyle()} is successfully spawned.");
            }
        }

        private void BootStructureModule(GameObject clone, ModuleSpawnSpecification specification)
        {
            Console.LogProgress();

            var configuration = new ModuleBootstrapConfiguration(_resolver, clone, specification);
            var bootstrap = new StructureModuleBootstrapFactory(configuration).Create();
            bootstrap.Boot();

            Console.LogEventMessage($"{"Module".ToBoldStyle()} is booting completely.");
        }
    }
}

#endif
