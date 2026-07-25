using Core.Utility.Input;

namespace Core.Object.Module.Booster
{
    public abstract class BoosterModuleModel : ModuleModel
    {
        private BoosterModuleInputActions _inputActions;

        protected BoosterModuleModel(ModuleRank rank, ModuleData data, BoosterModuleInputActions inputActions) : base(rank, data)
        {
            _inputActions = inputActions;
        }
    }
}
