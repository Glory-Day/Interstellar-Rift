namespace Core.Object.Service
{
    public interface ILocalServiceResolver
    {
        public TService GetLocalService<TService>() where TService : LocalService;
    }
}
