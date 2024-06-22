namespace Spacetraders.Api.Client;

public partial class SpacetradersApiClient
{
    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Token}");
    }
}
