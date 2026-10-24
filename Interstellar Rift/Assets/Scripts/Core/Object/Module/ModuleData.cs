using System;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// Represents a read-only data transfer object (DTO) for the stat data of a module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The values are configured only through Unity serialization, such as in the Inspector,
    /// and cannot be modified by other objects.
    /// </para>
    /// <para>
    /// The serialized fields are declared as protected, so that derived classes can extend
    /// this class with additional stats.
    /// </para>
    /// </remarks>
    [Serializable]
    public class ModuleData
    {
        #region SERIALIZABLE FIELD API

        [SerializeField] protected float mass;
        [SerializeField] protected float durability;

        #endregion

        /// <summary>
        /// Gets the mass of the module.
        /// </summary>
        public float Mass => mass;

        /// <summary>
        /// Gets the durability of the module.
        /// </summary>
        public float Durability => durability;
    }
}
