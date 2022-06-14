namespace EntityLibrary;

public class RecordSearch : BaseEntity
{
    public string BusinessName { get; set; } = string.Empty;
    public int BrandId { get; set; }
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
