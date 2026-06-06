using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

using Console = GloryDay.Debug.Console;

namespace Core.Utility.Addressables
{
    public class AddressableAssetPatcher
    {
        /// <summary>
        /// Checks whether there are any updatable catalogs on the server.
        /// </summary>
        /// <returns>A list of catalog IDs that need to be updated. Empty if already up to date.</returns>
        public async UniTask<IReadOnlyList<string>> CheckForUpdatesAsync()
        {
            var handle = UnityEngine.AddressableAssets.Addressables.CheckForCatalogUpdates(autoReleaseHandle: false);
            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Console.LogWarning("Failed to check for updates.");

                UnityEngine.AddressableAssets.Addressables.Release(handle);

                return Array.Empty<string>();
            }

            var result = new List<string>(handle.Result);
            UnityEngine.AddressableAssets.Addressables.Release(handle);

            return result;
        }

        /// <summary>
        /// Updates the specified list of catalogs. Pass the result of <see cref="CheckForUpdatesAsync"/> directly.
        /// </summary>
        /// <param name="catalogs">The list of catalog IDs to update.</param>
        public async UniTask PatchAsync(IReadOnlyList<string> catalogs)
        {
            if (catalogs == null || catalogs.Count == 0)
            {
                Console.LogMessage("Catalog is up to date.");

                return;
            }

            var handle = UnityEngine.AddressableAssets.Addressables.UpdateCatalogs(catalogs, autoReleaseHandle: false);
            await handle.ToUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Console.LogSuccess($"{handle.Result.Count} catalogs updated completely.");
            }
            else
            {
                Console.LogError("Failed to update catalogs. Using the cached bundle.");
            }

            UnityEngine.AddressableAssets.Addressables.Release(handle);
        }

        /// <summary>
        /// Initializes the Addressables runtime and updates catalogs.
        /// </summary>
        public async UniTask UpdateAsync()
        {
            var handle = UnityEngine.AddressableAssets.Addressables.InitializeAsync();
            await handle.ToUniTask();

            var catalogs = await CheckForUpdatesAsync();
            await PatchAsync(catalogs);

            IsReadyToPatch = true;
            OnReadyToPatched?.Invoke();

            Console.LogSuccess("Addressable assets initialized.");
        }

        /// <summary>
        /// Occurs when initialization and catalog update have completed.
        /// </summary>
        public event Action OnReadyToPatched;

        /// <summary>
        /// Handles bundle downloads per label.
        /// </summary>
        public AddressableAssetDownloader Downloader { get; } = new AddressableAssetDownloader();

        /// <summary>
        /// Indicates whether catalog initialization and update have completed.
        /// </summary>
        public bool IsReadyToPatch { get; private set; }
    }
}
