using System.Net.Http.Json;
using ModelLibrary.Interfaces;

namespace WebUI.Services;

public class DbValidator : IDbValidator
{
    private readonly HttpClient _client;

    public DbValidator(HttpClient client) => _client = client;

    public async Task<bool> IsBrandNameExists(
        string name, int id, CancellationToken ct)
        => await _client.GetFromJsonAsync<bool>(
            ApiRoutes.DbValidator.IsBrandNameExists +
                $"?name={name}&id={id}");

    public async Task<bool> IsUserEmailExists(
        string email, int id, CancellationToken ct)
        => await _client.GetFromJsonAsync<bool>(
            ApiRoutes.DbValidator.IsUserEmailExists +
                $"?email={email}&id={id}");
}
