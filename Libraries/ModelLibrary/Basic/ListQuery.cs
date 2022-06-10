namespace ModelLibrary.Basic;

public class ListQuery<TDto>
    where TDto : BaseDto
{
    public List<TDto> Items { get; set; } = new();
    public int Index { get; set; }
    public int Size { get; set; }
    public int TotalItems { get; set; }

    public ListQuery(
        List<TDto> items, int index, int totalItems)
    {
        Items.AddRange(items);
        Index = index;
        TotalItems = totalItems;
    }

    public ListQuery(List<TDto> items)
        => Items.AddRange(items);

    public ListQuery() { }
}
