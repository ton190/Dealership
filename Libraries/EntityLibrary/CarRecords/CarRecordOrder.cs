namespace EntityLibrary.CarRecords;

public class CarRecordOrder : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string RecordSearch { get; set; } = null!;
    public string SearchResult { get; set; } = null!;
    public bool Paid { get; set; }
}
