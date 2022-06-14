namespace EntityLibrary;

public class Order : BaseEntity
{
    public string SessionId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RecordSearch { get; set; } = null!;
    public string SearchResult { get; set; } = null!;
    public string Status { get; set; } = "unpaid";
}
