using Core.Utility.Input;

namespace Core.Object.Module.Booster
{
    public class BuiltInBoosterModuleModel : BoosterModuleModel
    {
        public BuiltInBoosterModuleModel(ModuleRank rank, ModuleData data, BoosterModuleInputActions inputActions) : base(rank, data, inputActions) { }

        public override string Name => ModuleNames.Booster.BuiltInBooster;
    }
}
