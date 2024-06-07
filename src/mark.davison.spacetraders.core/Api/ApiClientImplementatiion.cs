namespace mark.davison.spacetraders.core.Api;

public partial class SpaceTradersApiClient
{
    private readonly IAuthenticationContext _authenticationContext;

    public string Token { get; set; }

    public SpaceTradersApiClient(IAuthenticationContext authenticationContext, HttpClient client) : this(client)
    {
        _authenticationContext = authenticationContext;
    }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        request.Headers.Authorization = new("Bearer", string.IsNullOrEmpty(Token) ? _authenticationContext.Token : Token);
    }
}
