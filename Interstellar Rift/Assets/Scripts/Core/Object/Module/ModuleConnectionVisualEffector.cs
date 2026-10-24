using Core.Object.Service;
using GloryDay.Debug;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to start or stop the visual effect while a module is being dragged.
    /// </summary>
    public class ModuleConnectionVisualEffector : LocalService
    {
        #region GLOBAL SERVICE API

        private UpdateEventDispatcher _updateEventDispatcher;

        #endregion

        #region LOCAL SERVICE API

        private ElectricArcVisualEffect _electricArcVisualEffect;

        #endregion

        public ModuleConnectionVisualEffector(UpdateEventDispatcher updateEventDispatcher,
                                              ElectricArcVisualEffect electricArcVisualEffect)
        {
            Console.LogProgress();

            _updateEventDispatcher = updateEventDispatcher;

            _electricArcVisualEffect = electricArcVisualEffect;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            _updateEventDispatcher = null;

            _electricArcVisualEffect = null;
        }

        /// <summary>
        /// Begins the arc visual effect. Positions and activates the controller, then starts updating its
        /// middle points every frame.
        /// </summary>
        public void StartVisualEffect()
        {
            Console.LogProgress();

            _electricArcVisualEffect.UpdatePoints();
            _electricArcVisualEffect.Enable();

            _updateEventDispatcher.OnUpdate += _electricArcVisualEffect.UpdatePoints;

            IsUpdating = true;
        }

        /// <summary>
        /// Ends the arc visual effect. Deactivates the controller and stops updating its middle points every frame.
        /// </summary>
        public void StopVisualEffect()
        {
            Console.LogProgress();

            _electricArcVisualEffect.Disable();

            _updateEventDispatcher.OnUpdate -= _electricArcVisualEffect.UpdatePoints;

            IsUpdating = false;
        }

        /// <summary>
        /// Gets whether the arc visual effect is currently active and being updated every frame.
        /// </summary>
        public bool IsUpdating { get; private set; }
    }
}
