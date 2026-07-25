using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Core.Utility.Addressables
{
    public class AddressableAssetManager : IInitializable, IDisposable
    {
        private readonly List<IAddressableAssetLoader> _loaders = new List<IAddressableAssetLoader>();
        private readonly AddressableAssets _assets;

        private readonly AddressableAssetPatcher _patcher = new AddressableAssetPatcher();

        public AddressableAssetManager(AddressableAssets assets)
        {
            _assets = assets;

            //_loaders.Add(new AddressableAssetLoader<GameObject>(assets.Object_Internal));
            //_loaders.Add(new AddressableAssetLoader<GameObject>(assets.UI_Internal));
            //_loaders.Add(new AddressableAssetLoader<AudioClip>(assets.Audio_Internal));
            //_loaders.Add(new AddressableAssetLoader<Sprite>(assets.Image_Internal));
        }

        public void Initialize()
        {
            _patcher.UpdateAsync().Forget();
        }

        /// <summary>
        /// Loads assets sequentially through all registered loaders.
        /// </summary>
        public async UniTask LoadAllAssetsAsync()
        {
            foreach (var loader in _loaders)
            {
                await loader.LoadAllAssetsAsync();
            }
        }

        public void Dispose()
        {
            foreach (var loader in _loaders)
            {
                loader.ReleaseAllAssets();
            }
        }
    }
}
