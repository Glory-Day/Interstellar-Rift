using Core.Utility;
using Core.Utility.Attributes;
using Core.Utility.Extensions;
using GloryDay.Debug;

namespace Core.Object.Service
{
    /// <summary>
    /// Represents a local service whose functionality can be enabled and disabled.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The service is enabled when it is created. Calling <see cref="Enable"/> on an enabled service,
    /// or <see cref="Disable"/> on a disabled service, has no effect.
    /// </para>
    /// <para>
    /// Derived classes implement the behavior performed when the service is enabled or disabled
    /// in <see cref="Enable_Internal"/> and <see cref="Disable_Internal"/>. These methods are invoked
    /// only when the state actually changes, after <see cref="IsEnabled"/> has been updated.
    /// </para>
    /// <para>
    /// This class is marked with <see cref="ExcludeFromInstallationAttribute"/>, so a service derived
    /// from it cannot be resolved as <see cref="EnableableLocalService"/>.
    /// </para>
    /// </remarks>
    [ExcludeFromInstallation]
    public abstract class EnableableLocalService : LocalService, IEnableable
    {
        /// <inheritdoc/>
        public void Enable()
        {
            if (IsEnabled)
            {
                return;
            }

            IsEnabled = true;

            Enable_Internal();

            Console.LogMessage($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} has been enabled.");
        }

        /// <inheritdoc/>
        public void Disable()
        {
            if (IsEnabled == false)
            {
                return;
            }

            IsEnabled = false;

            Disable_Internal();

            Console.LogMessage($"{GetType().Name.ToNicifyPascalCase().ToBoldStyle()} has been disabled.");
        }

        /// <summary>
        /// Performs the behavior of this service when it changes from the disabled state to the enabled state.
        /// </summary>
        /// <remarks>
        /// This method is invoked by <see cref="Enable"/> only when the state actually changes,
        /// after <see cref="IsEnabled"/> has been set to <see langword="true"/>.
        /// </remarks>
        protected abstract void Enable_Internal();

        /// <summary>
        /// Performs the behavior of this service when it changes from the enabled state to the disabled state.
        /// </summary>
        /// <remarks>
        /// This method is invoked by <see cref="Disable"/> only when the state actually changes,
        /// after <see cref="IsEnabled"/> has been set to <see langword="false"/>.
        /// </remarks>
        protected abstract void Disable_Internal();

        /// <inheritdoc/>
        public bool IsEnabled { get; private set; } = true;
    }
}
