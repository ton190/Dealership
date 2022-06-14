namespace Shared;

public enum MemoryCacheNames
{
    Brands
}

public class CacheList : List<MemoryCacheNames>
{
    public CacheList(){}
    public CacheList(IEnumerable<MemoryCacheNames> names)
        => this.AddRange(names);
    public CacheList(MemoryCacheNames name)
        => this.Add(name);
}
