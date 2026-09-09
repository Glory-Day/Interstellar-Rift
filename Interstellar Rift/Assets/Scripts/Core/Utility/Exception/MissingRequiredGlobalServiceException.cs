namespace Core.Utility.Exception
{
    public class MissingRequiredGlobalServiceException : System.Exception
    {
        /// <inheritdoc/>
        public MissingRequiredGlobalServiceException(string globalServiceName)
            : base($"Required component '{globalServiceName}' was not provided before Build() was called.")
        {
            GlobalServiceName = globalServiceName;
        }

        /// <inheritdoc/>
        public MissingRequiredGlobalServiceException(string globalServiceName, string message)
            : base(message)
        {
            GlobalServiceName = globalServiceName;
        }

        /// <inheritdoc/>
        public MissingRequiredGlobalServiceException(string globalServiceName, string message, System.Exception innerException)
            : base(message, innerException)
        {
            GlobalServiceName = globalServiceName;
        }

        /// <summary>
        /// The name of the required global service that was missing at build time.
        /// </summary>
        public string GlobalServiceName { get; }
    }
}
