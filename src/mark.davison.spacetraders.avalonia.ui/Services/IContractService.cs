namespace mark.davison.spacetraders.avalonia.ui.Services;

public interface IContractService
{
    Task<ContractsResponse> GetContracts(int page, int limit, CancellationToken cancellationToken);
}
