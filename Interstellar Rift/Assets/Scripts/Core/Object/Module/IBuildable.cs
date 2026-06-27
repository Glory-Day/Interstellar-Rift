namespace Core.Object.Module
{
    public interface IBuildable<out T>
    {
        public T Build();
    }
}
