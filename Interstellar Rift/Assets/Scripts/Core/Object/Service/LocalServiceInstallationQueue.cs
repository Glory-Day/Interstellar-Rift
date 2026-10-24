using System;
using System.Collections.Generic;
using System.Linq;
 
namespace Core.Object.Service
{
    /// <summary>
    /// A queue that holds <see cref="LocalServiceFactoryAsset"/> instances grouped by <see cref="InstallationMode"/>
    /// and hands them out for installation.
    /// </summary>
    /// <remarks>
    /// Automatic and lazy installation assets are handed out in the order in which they appear in the list given to the constructor.
    /// </remarks>
    public class LocalServiceInstallationQueue
    {
        private readonly List<LocalServiceFactoryAsset> _manualInstallationAssets = new List<LocalServiceFactoryAsset>();
        private readonly Queue<LocalServiceFactoryAsset> _automaticInstallationAssets = new Queue<LocalServiceFactoryAsset>();
        private readonly Queue<LocalServiceFactoryAsset> _lazyInstallationAssets = new Queue<LocalServiceFactoryAsset>();
 
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServiceInstallationQueue"/> class
        /// by grouping the given assets according to their installation modes.
        /// </summary>
        /// <param name="assets">The assets to group by installation mode.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an asset specifies an undefined <see cref="InstallationMode"/>.</exception>
        public LocalServiceInstallationQueue(List<LocalServiceFactoryAsset> assets)
        {
            foreach (var asset in assets)
            {
                switch (asset.Mode)
                {
                    case InstallationMode.Manual:
                        _manualInstallationAssets.Add(asset);
                        break;
                    case InstallationMode.Automatic:
                        _automaticInstallationAssets.Enqueue(asset);
                        break;
                    case InstallationMode.Lazy:
                        _lazyInstallationAssets.Enqueue(asset);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(assets), asset.Mode, $"Undefined installation mode in '{asset.name}'.");
                }
            }
        }
 
        /// <summary>
        /// Attempts to remove and return the first manual installation asset of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the asset to take.</typeparam>
        /// <param name="asset">When this method returns, contains the taken asset if one was found; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if a matching asset was taken; otherwise, <see langword="false"/>.</returns>
        public bool TryTakeManualInstallationAsset<T>(out T asset) where T : LocalServiceFactoryAsset
        {
            asset = _manualInstallationAssets.OfType<T>().FirstOrDefault();
            if (asset == null)
            {
                return false;
            }
 
            _manualInstallationAssets.Remove(asset);
 
            return true;
        }
 
        /// <summary>
        /// Attempts to remove and return the next automatic installation asset.
        /// </summary>
        /// <param name="asset">When this method returns, contains the taken asset if one remained; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if an asset was taken; otherwise, <see langword="false"/>.</returns>
        public bool TryTakeAutomaticInstallationAsset(out LocalServiceFactoryAsset asset)
        {
            return _automaticInstallationAssets.TryDequeue(out asset);
        }
 
        /// <summary>
        /// Attempts to remove and return the next lazy installation asset.
        /// </summary>
        /// <param name="asset">When this method returns, contains the taken asset if one remained; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if an asset was taken; otherwise, <see langword="false"/>.</returns>
        public bool TryTakeLazyInstallationAsset(out LocalServiceFactoryAsset asset)
        {
            return _lazyInstallationAssets.TryDequeue(out asset);
        }
    }
}