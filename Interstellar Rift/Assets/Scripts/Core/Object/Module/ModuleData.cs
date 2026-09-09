using System;
using UnityEngine;

namespace Core.Object.Module
{
    [Serializable]
    public class ModuleData
    {
        #region SERIALIZABLE FIELD API

        [SerializeField] protected float mass;
        [SerializeField] protected float durability;

        #endregion

        public float Mass => mass;

        public float Durability => durability;
    }
}
