namespace ModelLibrary.Interfaces;

public interface IDbValidator
{
    Task<bool> IsBrandNameExists(string name, int id, CancellationToken ct);
    Task<bool> IsUserEmailExists(string email, int id, CancellationToken ct);
}
