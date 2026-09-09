using System.Collections.Generic;
using UnityEngine;

namespace Core.Utility.Addressables
{
    public interface IReadOnlyAddressableAssets
    {
        /// <summary>
        /// A dictionary of loaded <see cref="GameObject"/> assets.
        /// The key is the address assigned in Addressables.
        /// </summary>
        public IReadOnlyDictionary<string, GameObject> Object { get; }

        /// <summary>
        /// A dictionary of loaded UI <see cref="GameObject"/> assets.
        /// The key is the address assigned in Addressables.
        /// </summary>
        public IReadOnlyDictionary<string, GameObject> UI { get; }

        /// <summary>
        /// A dictionary of loaded <see cref="AudioClip"/> assets.
        /// The key is the address assigned in Addressables.
        /// </summary>
        public IReadOnlyDictionary<string, AudioClip> Audio { get; }

        /// <summary>
        /// A dictionary of loaded <see cref="Sprite"/> assets.
        /// The key is the address assigned in Addressables.
        /// </summary>
        public IReadOnlyDictionary<string, Sprite> Image { get; }
    }
}
