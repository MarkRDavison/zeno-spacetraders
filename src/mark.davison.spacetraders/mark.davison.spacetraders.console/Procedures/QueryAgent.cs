namespace mark.davison.spacetraders.console.Procedures;

public static class QueryAgent
{
    public static async Task<QueryAgentInfo> QueryAgentInfo(SpaceTradersApiClient api)
    {
        var info = await api.GetMyAgentAsync();
        await Task.Delay(1000);

        return new QueryAgentInfo(info.Data.Credits);
    }
}
