namespace Core.Utility.Pool.Exception
{
    /// <summary>
    /// Thrown when no container is found for the specified original object in the object pool.
    /// </summary>
    public class NotContainedException : System.Exception
    {
        /// <param name="name">The name of the original object for which no container was found.</param>
        public NotContainedException(string name) : base($"No container for the original object {name}.") { }
    }
}
