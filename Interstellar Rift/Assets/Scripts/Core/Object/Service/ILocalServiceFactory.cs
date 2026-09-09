namespace Core.Object.Service
{
    public interface ILocalServiceFactory
    {
        public LocalService Create(ServiceResolver resolver);
    }
}
