namespace Core.Object.Service
{
    /// <summary>
    /// Represents an object that requires initialization before it can be used.
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Initializes this object so it can begin using the local services it depends on.
        /// </summary>
        public void Initialize();
    }
}
