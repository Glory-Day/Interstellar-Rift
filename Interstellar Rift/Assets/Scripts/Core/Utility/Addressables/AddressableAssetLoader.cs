using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GloryDay.Debug;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Utility.Addressables
{
    public class AddressableAssetLoader<T> : IAddressableAssetLoader where T : UnityEngine.Object
    {
        private readonly Dictionary<string, AsyncOperationHandle<T>> _handles = new Dictionary<string, AsyncOperationHandle<T>>();
        private readonly string _label;

        private readonly Dictionary<string, T> _container;

        public AddressableAssetLoader(string label, Dictionary<string, T> container)
        {
            _container = container;
            _label = label;
        }

        public async UniTask LoadAssetAsync(string address)
        {
            if (_handles.ContainsKey(address))
            {
                Console.LogWarning($"{address} is already loaded.");

                return;
            }

            var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(address);
            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Console.LogError($"Load asset failed: {address}");

                UnityEngine.AddressableAssets.Addressables.Release(handle);

                return;
            }

            _handles.Add(address, handle);
            _container.Add(address, handle.Result);
        }

        public async UniTask LoadAllAssetsAsync()
        {
            var location = UnityEngine.AddressableAssets.Addressables.LoadResourceLocationsAsync(_label, typeof(T));
            await location.ToUniTask();

            if (location.Status != AsyncOperationStatus.Succeeded)
            {
                Console.LogError($"Location searching failed: {_label}");

                UnityEngine.AddressableAssets.Addressables.Release(location);

                return;
            }

            var locations = location.Result;
            UnityEngine.AddressableAssets.Addressables.Release(location);

            var count = locations.Count;
            for (var i = 0; i < count; i++)
            {
                var address = locations[i].PrimaryKey;
                if (_handles.ContainsKey(address))
                {
                    continue;
                }

                var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(locations[i]);
                await handle.ToUniTask();

                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Console.LogError($"Load asset failed: {address}");

                    UnityEngine.AddressableAssets.Addressables.Release(handle);

                    continue;
                }

                _handles.Add(address, handle);
                _container.Add(address, handle.Result);
            }
        }

        public void ReleaseAsset(string address)
        {
            if (_handles.Remove(address, out var handle) == false)
            {
                return;
            }

            _container.Remove(address);

            UnityEngine.AddressableAssets.Addressables.Release(handle);

            Console.LogSuccess($"Release asset succeeded: {address}");
        }

        public void ReleaseAllAssets()
        {
            foreach (var handle in _handles.Values)
            {
                UnityEngine.AddressableAssets.Addressables.Release(handle);
            }

            _handles.Clear();
            _container.Clear();

            Console.LogSuccess("Release all assets completed");
        }
    }
}
