namespace Core.Utility
{
    public interface ICacher<out TResult>
    {
        public TResult Fetch();

        public void Clear();

        public bool IsCached { get; }
    }
}
