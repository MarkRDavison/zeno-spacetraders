namespace mark.davison.spacetraders.desktop.ui.Services;

public interface IContractService
{
    Task<List<ContractDto>> FetchContracts(CancellationToken cancellationToken = default);
}
