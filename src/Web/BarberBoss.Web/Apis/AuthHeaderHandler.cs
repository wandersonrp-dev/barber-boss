using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BarberBoss.Web.Apis;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public AuthHeaderHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorageService.GetItemAsync<string>("auth_token");        

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
