namespace Core.Object.Module
{
    public interface IFactory<out T>
    {
        public T Create();
    }
}
