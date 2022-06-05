namespace ModelLibrary.Basic;

public abstract class BaseDto : IDto
{
    public int Id { get; set; }

    public object Clone() => this.MemberwiseClone();
}
