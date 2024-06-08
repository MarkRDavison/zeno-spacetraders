
namespace mark.davison.spacetraders.avalonia.ui.Services;

public sealed class ContractService : IContractService
{
    private readonly ISpaceTradersApiClient _apiClient;

    public ContractService(ISpaceTradersApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task<ContractsResponse> GetContracts(int page, int limit, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
