namespace mark.davison.spacetraders.desktop.ui.Services;

internal sealed class ContractService : IdentifiedService, IContractService
{
    public ContractService(
        IClientHttpRepository clientHttpRepository,
        IAccountService accountService
    ) : base(
        clientHttpRepository,
        accountService)
    {
    }

    public async Task<List<ContractDto>> FetchContracts(CancellationToken cancellationToken = default)
    {
        var response = await Get<FetchContractsQueryRequest, FetchContractsQueryResponse>(new(), cancellationToken);

        return response.SuccessWithValue
            ? response.Value
            : [];
    }
}
