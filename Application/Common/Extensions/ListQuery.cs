namespace Application.Extensions;

public static class Extensions
{
    public static async Task<ListQuery<TDto>> ToQueryListAsync<TDto>(
        this IQueryable<TDto> source, int index, int size,
        CancellationToken ct = default(CancellationToken))
        where TDto : BaseDto
    {
        var total = await source.CountAsync();
        if(total == 0) return new ListQuery<TDto>(new(), index, total);

        size = size > total ? total : size == 0 ? total : size;
        if (index < 0) index = 0;

        var items = await source
            .Skip(index)
            .Take(size)
            .ToListAsync();

        return new ListQuery<TDto>(items, index, total);
    }
}
