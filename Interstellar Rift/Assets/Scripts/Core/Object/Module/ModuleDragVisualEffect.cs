using Core.Object.Service;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to start or stop the visual effect while a module is being dragged.
    /// </summary>
    public class ModuleDragVisualEffect : LocalService
    {
        #region GLOBAL SERVICE API

        private UpdateEventDispatcher _updateEventDispatcher;

        #endregion

        #region LOCAL SERVICE API

        private ElectricArcVisualEffect _electricArcVisualEffect;

        #endregion

        public ModuleDragVisualEffect(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();

            _updateEventDispatcher = resolver.GetGlobalService<UpdateEventDispatcher>();

            _electricArcVisualEffect = resolver.GetLocalService<ElectricArcVisualEffect>();
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
        /// <param name="spawner">World position of the arc's start point (the dragged module's slot).</param>
        /// <param name="result">The target slot search result the arc should connect to.</param>
        public void StartVisualEffect(Vector3 spawner, SearchedSlotResult result)
        {
            _electricArcVisualEffect.UpdateTargetPoint(spawner, result);
            _electricArcVisualEffect.UpdateMiddlePoints();
            _electricArcVisualEffect.Enable();

            _updateEventDispatcher.OnUpdate += _electricArcVisualEffect.UpdateMiddlePoints;

            IsUpdating = true;
        }

        /// <summary>
        /// Updates the arc's target while the drag is in progress, without changing its updating state.
        /// </summary>
        /// <param name="spawner">World position of the arc's start point (the dragged module's slot).</param>
        /// <param name="result">The target slot search result the arc should connect to.</param>
        public void UpdateVisualEffect(Vector3 spawner, SearchedSlotResult result)
        {
            _electricArcVisualEffect.UpdateTargetPoint(spawner, result);
        }

        /// <summary>
        /// Ends the arc visual effect. Deactivates the controller and stops updating its middle points every frame.
        /// </summary>
        public void StopVisualEffect()
        {
            _electricArcVisualEffect.Disable();

            _updateEventDispatcher.OnUpdate -= _electricArcVisualEffect.UpdateMiddlePoints;

            IsUpdating = false;
        }

        /// <summary>
        /// Gets whether the arc visual effect is currently active and being updated every frame.
        /// </summary>
        public bool IsUpdating { get; private set; } = false;
    }
}
