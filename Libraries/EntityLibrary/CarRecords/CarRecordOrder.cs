namespace EntityLibrary.CarRecords;

public class CarRecordOrder : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string RecordSearch { get; set; } = null!;
    public string SearchResult { get; set; } = null!;
    public bool Paid { get; set; }
}

public class CarRecordSearch : BaseEntity
{
    public string BusinessName { get; set; } = string.Empty;
    public int CarBrandId { get; set; }
    public SearchAddress BusinessAddress { get; set; } = new();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class SearchAddress
{
    public string PostalCode { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
    public string UnitNumber { get; set; } = string.Empty;
}
