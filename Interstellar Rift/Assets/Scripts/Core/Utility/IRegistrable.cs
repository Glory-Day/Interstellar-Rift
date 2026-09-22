namespace Core.Utility
{
    public interface IRegistrable<in TKey, TValue>
    {
        public void Register(TKey key, TValue value);

        public void UnRegister(TKey key);

        public bool ContainsKey(TKey key);

        public TValue Fetch(TKey key);

        public bool TryFetch(TKey key, out TValue value);
    }
}
