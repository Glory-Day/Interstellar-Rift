using UnityEngine;

namespace Core.Utility.Pool
{
    /// <summary>
    /// Configures the behavior of an object pool, such as its capacity limits and prewarming.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The original prefab to be cloned when the object pool creates a new instance.
        /// </summary>
        public GameObject Origin { get; internal set; }

        /// <summary>
        /// The initial capacity of the object pool.
        /// The pool pre-allocates internal storage for this number of objects on creation.
        /// The default value is 10.
        /// </summary>
        public int DefaultCapacity { get; internal set; } = 10;

        /// <summary>
        /// The maximum number of objects the pool retains in reserve.
        /// Objects released beyond this limit are destroyed immediately rather than returned to the pool.
        /// The default value is 50.
        /// </summary>
        public int MaximumSize { get; internal set; } = 50;

        /// <summary>
        /// The number of objects to instantiate and immediately return to the pool on registration.
        /// Use this to avoid instantiation spikes during gameplay.
        /// The default value is 0.
        /// </summary>
        public int PrewarmCount { get; internal set; } = 0;

        /// <summary>
        /// Whether to perform a collection check when releasing an object to the pool.
        /// If <see langword="true"/>, an exception is thrown when releasing an object that is already in the pool.
        /// This option is recommended for debug builds only.
        /// The default value is <see langword="false"/>.
        /// </summary>
        public bool IsCollectionChecked { get; internal set; } = false;
    }
}
