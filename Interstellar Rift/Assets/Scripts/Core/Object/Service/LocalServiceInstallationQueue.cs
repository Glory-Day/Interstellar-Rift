using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Object.Service
{
    public class LocalServiceInstallationQueue
    {
        private readonly List<LocalServiceFactoryAsset> _manualInstallationAssets = new List<LocalServiceFactoryAsset>();
        private readonly Queue<LocalServiceFactoryAsset> _automaticInstallationAssets = new Queue<LocalServiceFactoryAsset>();
        private readonly Queue<LocalServiceFactoryAsset> _lazyInstallationAssets = new Queue<LocalServiceFactoryAsset>();

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
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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

        public bool TryTakeAutomaticInstallationAsset(out LocalServiceFactoryAsset asset)
        {
            return _automaticInstallationAssets.TryDequeue(out asset);
        }

        public bool TryTakeLazyInstallationAsset(out LocalServiceFactoryAsset asset)
        {
            return _lazyInstallationAssets.TryDequeue(out asset);
        }
    }
}
