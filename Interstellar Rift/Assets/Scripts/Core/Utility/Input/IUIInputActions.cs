namespace Core.Utility.Input
{
    /// <summary>
    /// Marker interface for <see cref="IInputActions"/> belonging to <see cref="InputMode.UI"/>.
    /// Used only for dependency injection registration and type constraints.
    /// </summary>
    public interface IUIInputActions : IInputActions
    {
    }
}
