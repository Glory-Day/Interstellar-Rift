namespace Core.Object.Module
{
    /// <summary>
    /// Represents an object that requires a one-time boot step before it becomes usable.
    /// </summary>
    public interface IBootable
    {
        /// <summary>
        /// Performs a one-time boot step.
        /// </summary>
        public void Boot();
    }
}
