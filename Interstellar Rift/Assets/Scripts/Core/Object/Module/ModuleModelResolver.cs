using Core.Object.Service;
using GloryDay.Debug;

namespace Core.Object.Module
{
    public class ModuleModelResolver : LocalService
    {
        public ModuleModelResolver(ModuleModel model)
        {
            Console.LogProgress();

            Model = model;
        }

        public override void Dispose()
        {
            Console.LogProgress();

            Model = null;
        }

        public ModuleModel Model { get; private set; }
    }
}
