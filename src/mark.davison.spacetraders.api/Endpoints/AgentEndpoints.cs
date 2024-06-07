using Microsoft.AspNetCore.Mvc;

namespace mark.davison.spacetraders.api.Endpoints;

public static class AgentEndpoints
{
    public static IEndpointRouteBuilder UseAgentRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/agents", async (
            HttpContext context,
            [FromServices]
            ISpaceTradersApiClient apiClient) =>
        {
            var registerBody = new RegisterBody
            {
                Symbol = "ZENOTIA",
                Email = "markdavison0@gmail.com",
                Faction = FactionSymbol.COSMIC
            };

            var registerResponse = await apiClient.RegisterAsync(registerBody);

            if (string.IsNullOrEmpty(registerResponse.Data.Token))
            {
                return Results.BadRequest();
            }

            return Results.Ok(registerResponse.Data);
        });

        endpoints.MapGet("/api/agents", async (
            HttpContext context,
            [FromServices]
            ISpaceTradersApiClient apiClient) =>
        {
            var tokenParts = context.Request.Headers.Authorization.ToString().Split(" ");
            apiClient.Token = tokenParts[1];

            var agentsResponse = await apiClient.GetAgentsAsync(null, null);

            return Results.Ok(agentsResponse.Data);
        });

        endpoints.MapGet("/api/agents/my", async (
            HttpContext context,
            [FromServices]
            ISpaceTradersApiClient apiClient) =>
        {
            var tokenParts = context.Request.Headers.Authorization.ToString().Split(" ");
            apiClient.Token = tokenParts[1];

            var myAgentResponse = await apiClient.GetMyAgentAsync();

            return Results.Ok(myAgentResponse.Data);
        });

        return endpoints;
    }
}
