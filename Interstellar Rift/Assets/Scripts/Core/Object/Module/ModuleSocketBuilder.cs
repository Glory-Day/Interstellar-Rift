using System.Collections.Generic;
using Core.Object.Service;
using Core.Utility;
using Core.Utility.Extension;
using GloryDay.Debug;

namespace Core.Object.Module
{
    public class ModuleSocketBuilder : LocalService, IBuildable<ModuleSocket>
    {
        private List<Slot> _slots;
        private Slot _joint;

        public ModuleSocketBuilder(ServiceResolver resolver) : base(resolver)
        {
            Console.LogProgress();
        }

        public override void Dispose()
        {
            Console.LogProgress();

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

        public ModuleSocket Build()
        {
            Console.LogProgress();

            Console.LogSuccess($"{nameof(ModuleSocket).ToNicifyPascalCase().ToBoldStyle()} is built.");

            return new ModuleSocket(_slots, _joint, Resolver);
        }
    }
}
