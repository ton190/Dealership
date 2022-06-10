namespace Application.Extensions;

public static class Extensions
{
    public static async Task<ListQuery<TDto>> ToQueryListAsync<TDto>(
        this IQueryable<TDto> source, int index, int size)
        where TDto : BaseDto
    {
        var total = await source.CountAsync();
        if(size == 0) size = total;

        if (index < 0) index = 0;

        var items = await source
            .Skip(index)
            .Take(size)
            .ToListAsync();

        return new ListQuery<TDto>(items, index, total);
    }
}
