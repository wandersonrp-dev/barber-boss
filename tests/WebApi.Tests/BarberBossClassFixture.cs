using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Tests;
public class BarberBossClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public BarberBossClassFixture(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();      
    }

    protected async Task<HttpResponseMessage> PostAsync(string requestUri, object request, string? token = null)
    {
        AuthorizeRequest(token);

        return await _httpClient.PostAsJsonAsync(requestUri, request);
    }

    protected async Task<HttpResponseMessage> GetAsync(string requestUri, string? token = null)
    {
        AuthorizeRequest(token);

        return await _httpClient.GetAsync(requestUri);
    }

    protected async Task<HttpResponseMessage> PatchAsync(string requestUri, object? request = null, string? token = null)
    {
        AuthorizeRequest(token);

        return await _httpClient.PatchAsJsonAsync(requestUri, request);
    }

    private void AuthorizeRequest(string? token)
    {
        if(!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
