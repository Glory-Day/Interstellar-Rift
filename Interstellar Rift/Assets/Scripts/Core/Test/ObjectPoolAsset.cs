using System;
using UnityEngine;

namespace Core.Test
{
    [Serializable]
    public struct ObjectPoolAsset
    {
        public GameObject asset;
        public int count;
    }
}
