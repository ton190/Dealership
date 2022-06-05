using System.Net.Http.Json;
using ModelLibrary.Interfaces;

namespace WebUI.Services;

public class DbValidator : IDbValidator
{
    private readonly HttpClient _client;

    public DbValidator(HttpClient client) => _client = client;

    public async Task<bool> IsCarBrandNameExists(
        string name, int id, CancellationToken ct)
        => await _client.GetFromJsonAsync<bool>(
            ApiRoutes.DbValidator.IsCarBrandNameExists +
                $"?name={name}&id={id}");
}
