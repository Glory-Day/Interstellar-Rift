using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    /// <summary>
    /// A service that provides the functionality to start or stop the visual effect while a module is being dragged.
    /// </summary>
    public class ModuleDragEffector : LocalServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Assets")]
        [Tooltip("The electric arc VFX controller whose lifecycle and target position this effector drives.")]
        [SerializeField] private ElectricArcController controller;

        #endregion

        #region SERVICE FIELD API

        private UpdateEventHandler _updateEventHandler;

        #endregion

        /// <inheritdoc/>
        public override void Initialize()
        {
            _updateEventHandler = Resolver.GetGlobalService<UpdateEventHandler>();

            base.Initialize();
        }

        /// <summary>
        /// Begins the arc visual effect. Positions and activates the controller, then starts updating its
        /// middle points every frame.
        /// </summary>
        /// <param name="spawner">World position of the arc's start point (the dragged module's slot).</param>
        /// <param name="result">The target slot search result the arc should connect to.</param>
        public void StartVisualEffect(Vector3 spawner, SearchedSlotResult result)
        {
            controller.UpdateTargetPoint(spawner, result);
            controller.UpdateMiddlePoints();
            controller.gameObject.SetActive(true);

            _updateEventHandler.OnUpdate += controller.UpdateMiddlePoints;

            IsUpdating = true;
        }

        /// <summary>
        /// Updates the arc's target while the drag is in progress, without changing its updating state.
        /// </summary>
        /// <param name="spawner">World position of the arc's start point (the dragged module's slot).</param>
        /// <param name="result">The target slot search result the arc should connect to.</param>
        public void UpdateVisualEffect(Vector3 spawner, SearchedSlotResult result)
        {
            controller.UpdateTargetPoint(spawner, result);
        }

        /// <summary>
        /// Ends the arc visual effect. Deactivates the controller and stops updating its middle points every frame.
        /// </summary>
        public void StopVisualEffect()
        {
            controller.gameObject.SetActive(false);

            _updateEventHandler.OnUpdate -= controller.UpdateMiddlePoints;

            IsUpdating = false;
        }

        /// <summary>
        /// Gets whether the arc visual effect is currently active and being updated every frame.
        /// </summary>
        public bool IsUpdating { get; private set; } = false;
    }
}
