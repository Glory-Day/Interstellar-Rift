using Core.Object.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleDragEffector : LocalServiceBehaviour
    {
        #region SERIALIZABLE FIELD API

        [Title("Assets")]
        [SerializeField] private ElectricArcController controller;

        #endregion

        private UpdateEventHandler _updateEventHandler;

        public override void Initialize()
        {
            _updateEventHandler = Resolver.GetGlobalService<UpdateEventHandler>();

            base.Initialize();
        }

        public void StartVisualEffect(Vector3 spawner, SearchedSlotResult result)
        {
            controller.UpdateTargetPoint(spawner, result);
            controller.UpdateMiddlePoints();
            controller.gameObject.SetActive(true);

            _updateEventHandler.OnUpdate += controller.UpdateMiddlePoints;

            IsUpdating = true;
        }

        public void UpdateVisualEffect(Vector3 spawner, SearchedSlotResult result)
        {
            controller.UpdateTargetPoint(spawner, result);
        }

        public void StopVisualEffect()
        {
            controller.gameObject.SetActive(false);

            _updateEventHandler.OnUpdate -= controller.UpdateMiddlePoints;

            IsUpdating = false;
        }

        public bool IsUpdating { get; private set; } = false;
    }
}
