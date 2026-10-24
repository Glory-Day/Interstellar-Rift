using System.Collections.Generic;
using Core.Object.Service;
using Core.Utility;
using Core.Utility.Exceptions;
using GloryDay.Debug;
using UnityEngine;

namespace Core.Object.Module
{
    public class ModuleSocketBuilder : LocalServiceBuilder<ModuleSocket>
    {
        #region LOCAL SERVICE API

        private Transform _transform;

        #endregion

        private List<Slot> _slots;
        private Slot _joint;

        public ModuleSocketBuilder(Transform transform)
        {
            Console.LogProgress();

            _transform = transform;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            base.Dispose();

            _slots = null;
            _joint = null;
        }

        public ModuleSocketBuilder WithSlots(List<Slot> slots)
        {
            _slots = slots;

            return this;
        }

        public ModuleSocketBuilder WithJoint(Slot joint)
        {
            _joint = joint;

            return this;
        }

        protected override void Validate()
        {
            if (_slots == null)
            {
                throw new MissingBuildComponentException(typeof(ModuleSocket), "Slots");
            }
        }

        protected override ModuleSocket Create()
        {
            Console.LogProgress();

            return new ModuleSocket(_slots, _joint, _transform);
        }
    }
}
