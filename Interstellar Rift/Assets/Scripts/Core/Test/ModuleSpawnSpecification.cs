using System;
using Core.Object.Module;
using Core.Object.Service;
using UnityEngine;

namespace Core.Test
{
    [Serializable]
    public struct ModuleSpawnSpecification
    {
        public ModuleRank rank;
        public GameObject prefab;
        public ModuleDataTable database;
        public Transform spawner;
        public LocalServiceRegistryBuilderFactoryAsset services;
    }
}
