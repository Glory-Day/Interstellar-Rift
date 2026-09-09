using System.Collections.Generic;
using UnityEngine;

namespace Core.Utility.Addressables
{
    public class AddressableAssets : IReadOnlyAddressableAssets
    {
        private readonly Dictionary<string, GameObject> _object = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, GameObject> _ui =  new Dictionary<string, GameObject>();
        private readonly Dictionary<string, AudioClip> _audio =  new Dictionary<string, AudioClip>();
        private readonly Dictionary<string, Sprite> _image =  new Dictionary<string, Sprite>();

        internal Dictionary<string, GameObject> Object_Internal => _object;

        internal Dictionary<string, GameObject> UI_Internal => _ui;

        internal Dictionary<string, AudioClip> Audio_Internal => _audio;

        internal Dictionary<string, Sprite> Image_Internal => _image;

        public IReadOnlyDictionary<string, GameObject> Object => _object;

        public IReadOnlyDictionary<string, GameObject> UI => _ui;

        public IReadOnlyDictionary<string, AudioClip> Audio => _audio;

        public IReadOnlyDictionary<string, Sprite> Image => _image;
    }
}
