namespace ModelLibrary.Interfaces;

public interface IDbValidator
{
    Task<bool> IsCarBrandNameExists(string name, int id, CancellationToken ct);
}
