namespace margarita.Service;

internal class Cache<T>
{
    private readonly Dictionary<Guid, T> _cache = new();

    public IReadOnlyCollection<T> GetAll()
    {
        return _cache.Values;
    }

    public T? Get(Guid id)
    {
        _cache.TryGetValue(id, out var value);
        return value;
    }

    public void Create(Guid id, T value)
    {
        _cache.Add(id, value);
    }

    public void Clear()
    {
        _cache.Clear();
    }
}
