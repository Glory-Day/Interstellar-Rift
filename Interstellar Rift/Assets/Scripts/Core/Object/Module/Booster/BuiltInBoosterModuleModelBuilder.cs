using Core.Utility.Exceptions;
using Core.Utility.Extensions;
using Core.Utility.Input;
using GloryDay.Debug;

namespace Core.Object.Module.Booster
{
    public class BuiltInBoosterModuleModelBuilder : ModuleModelBuilder
    {
        private BoosterModuleInputActions _inputActions;

        public BuiltInBoosterModuleModelBuilder(ModuleRank rank, ModuleData data) : base(rank, data) { }

        public BuiltInBoosterModuleModelBuilder WithInputActions(BoosterModuleInputActions inputActions)
        {
            Console.LogProgress();

            _inputActions = inputActions;

            return this;
        }

        protected override void Validate()
        {
            if (_inputActions == null)
            {
                throw new MissingBuildComponentException(typeof(BuiltInBoosterModuleModel), "Booster Module Input Actions");
            }
        }

        protected override ModuleModel Create()
        {
            Console.LogProgress();

            return new BuiltInBoosterModuleModel(Rank, Data, _inputActions);
        }
    }
}
