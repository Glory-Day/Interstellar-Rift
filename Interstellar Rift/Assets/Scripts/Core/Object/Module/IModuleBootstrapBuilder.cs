using Core.Utility;

namespace Core.Object.Module
{
    public interface IModuleBootstrapBuilder<out TModuleBoostrap, TBuilder> : IBuildable<TModuleBoostrap> { }
}
