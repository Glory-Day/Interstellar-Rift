using Core.Object.Service;
using UnityEngine;
using UnityEngine.EventSystems;

using Console = GloryDay.Debug.Console;

namespace Core.Object.Module
{
    public class ModuleDragEventHandler : LocalServiceBehaviour
    {
        private ModuleConnector _connector;
        private ModuleSearcher _searcher;
        private ModuleDragMovement _movement;
        private ModuleDragEffector _effector;

        private MouseButtonEventHandler _handler;

        public override void Initialize()
        {
            Console.LogProgress();

            _connector = Resolver.GetLocalService<ModuleConnector>();
            _searcher = Resolver.GetLocalService<ModuleSearcher>();
            _movement = Resolver.GetLocalService<ModuleDragMovement>();
            _effector = Resolver.GetLocalService<ModuleDragEffector>();

            _handler = Resolver.GetLocalService<MouseButtonEventHandler>();
            _handler.OnMouseButtonPressed += BeginMoving;
            _handler.OnMouseButtonPressed += Search;
            _handler.OnMouseButtonDragged += Move;
            _handler.OnMouseButtonDragged += Search;
            _handler.OnMouseButtonReleased += EndMoving;
            _handler.OnMouseButtonReleased += EndVisualEffect;

            base.Initialize();
        }

        private void OnDestroy()
        {
            Console.LogProgress();

            _handler.OnMouseButtonPressed -= BeginMoving;
            _handler.OnMouseButtonPressed -= Search;
            _handler.OnMouseButtonDragged -= Move;
            _handler.OnMouseButtonDragged -= Search;
            _handler.OnMouseButtonReleased -= EndMoving;
            _handler.OnMouseButtonReleased -= EndVisualEffect;
        }

        private void BeginMoving(PointerEventData eventData)
        {
            _movement.BeginMoving(eventData.position);
        }

        private void Move(PointerEventData eventData)
        {
            _movement.Move(eventData.position);
        }

        private void EndMoving(PointerEventData eventData)
        {
            _movement.EndMoving();
        }

        private void Search(PointerEventData eventData)
        {
            var result = _searcher.Search();
            var spawner = _connector.Joint.transform.position;;

            if (result.HasValue)
            {
                if (_effector.IsUpdating == false)
                {
                    _effector.StartVisualEffect(spawner, result.Value);
                }
            }
            else
            {
                if (_effector.IsUpdating)
                {
                    _effector.StopVisualEffect();
                }

                return;
            }

            var slot = result.Value.Slot;
            var target = slot.transform.position;

            _effector.UpdateVisualEffect(spawner, result.Value);
            _movement.Rotate(target);
        }

        private void EndVisualEffect(PointerEventData eventData)
        {
            _effector.StopVisualEffect();
        }
    }
}
