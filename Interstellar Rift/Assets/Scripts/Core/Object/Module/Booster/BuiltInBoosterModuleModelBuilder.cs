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

        public override ModuleModel Build()
        {
            Console.LogProgress();

            return new BuiltInBoosterModuleModel(Rank, Data, _inputActions);
        }
    }
}
