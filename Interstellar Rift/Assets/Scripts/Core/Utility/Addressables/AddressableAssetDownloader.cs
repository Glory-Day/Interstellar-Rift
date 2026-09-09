using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

using Console = GloryDay.Debug.Console;

namespace Core.Utility.Addressables
{
    public sealed class AddressableAssetDownloader
    {
        /// <summary>
        /// Returns the size in bytes of bundles not yet downloaded for the specified label. Returns 0 if already cached.
        /// </summary>
        /// <param name="label">The Addressables label to check.</param>
        public async UniTask<long> GetDownloadSizeAsync(string label)
        {
            var handle = UnityEngine.AddressableAssets.Addressables.GetDownloadSizeAsync(label);
            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                UnityEngine.AddressableAssets.Addressables.Release(handle);

                return 0L;
            }

            var size = handle.Result;
            UnityEngine.AddressableAssets.Addressables.Release(handle);

            return size;
        }

        /// <summary>
        /// Returns the list of file names to be downloaded for the specified label.
        /// </summary>
        /// <param name="label">The Addressables label to check.</param>
        public async UniTask<IReadOnlyList<string>> GetFileNamesAsync(string label)
        {
            var handle = UnityEngine.AddressableAssets.Addressables.LoadResourceLocationsAsync(label);
            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                UnityEngine.AddressableAssets.Addressables.Release(handle);

                return Array.Empty<string>();
            }

            var names = handle.Result.Select(location => System.IO.Path.GetFileName(location.InternalId)).Distinct().ToList();
            UnityEngine.AddressableAssets.Addressables.Release(handle);

            return names;
        }

        /// <summary>
        /// Downloads the bundle for the specified label.
        /// Progress is reported via <see cref="OnProgressChanged"/>.
        /// </summary>
        /// <param name="label">The Addressables label to download.</param>
        public async UniTask DownloadLabeledAssetsAsync(string label)
        {
            var fileNames = (await GetFileNamesAsync(label)).ToList();
            var total = fileNames.Count;

            var handle = UnityEngine.AddressableAssets.Addressables.DownloadDependenciesAsync(label, autoReleaseHandle: false);
            while (handle.IsDone == false)
            {
                var percent = handle.GetDownloadStatus().Percent;
                var count = Mathf.FloorToInt(percent * total);
                var progress = new Progress(percent, count, total);

                OnProgressChanged?.Invoke(new ProgressInformation(label, fileNames, progress));

                await UniTask.Yield();
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var progress = new Progress(1f, total, total);

                OnProgressChanged?.Invoke(new ProgressInformation(label, fileNames, progress));
                OnLabeledAssetsDownloaded?.Invoke(label);

                Console.LogSuccess($"Download labeled assets succeeded: {label}");
            }
            else
            {
                Console.LogError($"Download labeled assets failed: {label}");
            }

            UnityEngine.AddressableAssets.Addressables.Release(handle);
        }

        /// <summary>
        /// Downloads the specified list of labels in order.
        /// Progress is reported via <see cref="OnProgressChanged"/>.
        /// </summary>
        /// <param name="labels">The list of Addressables labels to download.</param>
        public async UniTask DownloadAllAssetsAsync(IReadOnlyList<string> labels)
        {
            if (IsDownloading)
            {
                Console.LogWarning("Download is already in progress.");

                return;
            }

            IsDownloading = true;

            try
            {
                foreach (var label in labels)
                {
                    await DownloadLabeledAssetsAsync(label);
                }

                OnAllAssetsDownloaded?.Invoke();

                Console.LogSuccess("Download all assets succeeded.");
            }
            finally
            {
                IsDownloading = false;
            }
        }

        /// <summary>
        /// Occurs when the download progress changes.
        /// </summary>
        public event Action<ProgressInformation> OnProgressChanged;

        /// <summary>
        /// Occurs when the download for a single label has completed.
        /// </summary>
        public event Action<string> OnLabeledAssetsDownloaded;

        /// <summary>
        /// Occurs when all label downloads have completed.
        /// </summary>
        public event Action OnAllAssetsDownloaded;

        /// <summary>
        /// Indicates whether a download is currently in progress.
        /// </summary>
        public bool IsDownloading { get; private set; }

        /// <summary>
        /// Represents the download progress information per label.
        /// </summary>
        public readonly struct ProgressInformation
        {
            /// <summary>
            /// The label being downloaded.
            /// </summary>
            public readonly string Label;

            /// <summary>
            /// The list of file names to be downloaded.
            /// </summary>
            public readonly IReadOnlyList<string> FileNames;

            /// <summary>
            /// The current download progress.
            /// </summary>
            public readonly Progress Progress;

            /// <param name="label">The label being downloaded.</param>
            /// <param name="fileNames">The list of file names.</param>
            /// <param name="progress">The progress state.</param>
            public ProgressInformation(string label, IReadOnlyList<string> fileNames, Progress progress)
            {
                Label = label;
                FileNames = fileNames;
                Progress = progress;
            }
        }

        /// <summary>
        /// Represents the download percentage and file counts.
        /// </summary>
        public readonly struct Progress
        {
            /// <summary>
            /// The overall download progress. (0.0 to 1.0)
            /// </summary>
            public readonly float Percent;

            /// <summary>
            /// The number of files downloaded so far.
            /// </summary>
            public readonly int DownloadedCount;

            /// <summary>
            /// The total number of files to download.
            /// </summary>
            public readonly int TotalCount;

            /// <param name="percent">Progress percentage (0.0 to 1.0).</param>
            /// <param name="downloadedCount">Number of completed files.</param>
            /// <param name="totalCount">Total number of files.</param>
            public Progress(float percent, int downloadedCount, int totalCount)
            {
                Percent = percent;
                DownloadedCount = downloadedCount;
                TotalCount = totalCount;
            }
        }
    }
}
