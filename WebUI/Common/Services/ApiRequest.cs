using System.Net.Http.Json;

namespace WebUI.Services;

public class ApiRequest
{
    private readonly HttpClient _httpClient;
    public ApiRequest(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<TResponse?> PostAsync<TResponse>(
        string route, object model) => await ReadDataAsync<TResponse>(
            await _httpClient.PostAsJsonAsync(route, model));

    public async Task<TResponse?> PutAsync<TResponse>(
            string route, object model) => await ReadDataAsync<TResponse>(
                await _httpClient.PutAsJsonAsync(route, model));

    public async Task<TResponse?> DeleteAsync<TResponse>(string route)
        => await ReadDataAsync<TResponse>(
                await _httpClient.DeleteAsync(route));

    public async Task<TResponse?> GetAsync<TResponse>(
        string route, CancellationToken ct)
        => await _httpClient.GetFromJsonAsync<TResponse>(route, ct);

    private async Task<TResponse?> ReadDataAsync<TResponse>(
        HttpResponseMessage message)
    {
        if (!message.IsSuccessStatusCode) return default(TResponse);

        return await message.Content.ReadFromJsonAsync<TResponse>();
    }
}
