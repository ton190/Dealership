namespace EntityLibrary.CarRecords;

public class CarBrand : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public virtual List<CarRecord> CarRecords { get; set; } = new();
}
