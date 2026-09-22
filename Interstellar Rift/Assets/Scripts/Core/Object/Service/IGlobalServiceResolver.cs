namespace Core.Object.Service
{
    public interface IGlobalServiceResolver
    {
        public TService GetGlobalService<TService>();
    }
}
