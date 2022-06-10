using EntityLibrary.CarRecords;

namespace ModelLibrary.CarRecords;

public class CarRecordOrderDto
    : BaseDto, IMap<CarRecordOrder, CarRecordOrderDto>
{
    public string? RecordSearch { get; set; }
    public string? SearchResult { get; set; }
    public string Email { get; set; } = string.Empty;

    public bool Paid { get; set; }
}
