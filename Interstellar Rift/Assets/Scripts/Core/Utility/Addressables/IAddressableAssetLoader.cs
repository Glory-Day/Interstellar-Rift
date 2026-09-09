using Cysharp.Threading.Tasks;

namespace Core.Utility.Addressables
{
    public interface IAddressableAssetLoader
    {
        /// <summary>
        /// Asynchronously loads the asset at the specified address.
        /// </summary>
        /// <param name="address">The address assigned in Addressables.</param>
        public UniTask LoadAssetAsync(string address);

        /// <summary>
        /// Asynchronously loads all assets belonging to the label specified in the constructor.
        /// </summary>
        public UniTask LoadAllAssetsAsync();

        /// <summary>
        /// Releases the asset at the specified address from memory.
        /// </summary>
        /// <param name="address">The address assigned in Addressables.</param>
        public void ReleaseAsset(string address);

        /// <summary>
        /// Releases all loaded assets from memory.
        /// </summary>
        public void ReleaseAllAssets();
    }
}
