namespace Core.Utility.Exception
{
    public class MissingRequiredLocalServiceException : System.Exception
    {
        /// <inheritdoc/>
        public MissingRequiredLocalServiceException(string localServiceName)
            : base($"Required component '{localServiceName}' was not provided before Build() was called.")
        {
            LocalServiceName = localServiceName;
        }

        /// <inheritdoc/>
        public MissingRequiredLocalServiceException(string localServiceName, string message)
            : base(message)
        {
            LocalServiceName = localServiceName;
        }

        /// <inheritdoc/>
        public MissingRequiredLocalServiceException(string localServiceName, string message, System.Exception innerException)
            : base(message, innerException)
        {
            LocalServiceName = localServiceName;
        }

        /// <summary>
        /// The name of the required component that was missing at build time.
        /// </summary>
        public string LocalServiceName { get; }
    }
}
