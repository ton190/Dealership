namespace ModelLibrary.Basic;

public abstract class BaseDto : IDto
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public object Clone() => this.MemberwiseClone();
}
