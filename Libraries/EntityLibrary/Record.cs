using Domain.Enums;

namespace EntityLibrary;

public class Record : BaseEntity
{
    public string BusinessName { get; set; } = string.Empty;
    public string FINCode { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public Address BusinessAddress { get; set; } = new();
    public List<Phone> PhoneNumbers { get; set; } = new();
    public List<ContactName> ContactNames { get; set; } = new();
}

public class Address
{
    public ProvinceEnum Province { get; set; } = ProvinceEnum.None;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
    public string UnitNumber { get; set; } = string.Empty;
}

public class ContactName : BaseEntity
{
    public int RecordId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class Phone : BaseEntity
{
    public int RecordId { get; set; }
    public string Number { get; set; } = string.Empty;
}

