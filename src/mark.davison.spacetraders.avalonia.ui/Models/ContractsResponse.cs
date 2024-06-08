namespace mark.davison.spacetraders.avalonia.ui.Models;

public class ContractsResponse
{
    public ContractsResponse(
        Meta meta,
         IEnumerable<ContractInfoModel> contracts)
    {
        Meta = meta;
        Contracts = [.. contracts];
    }

    public Meta Meta { get; }
    public List<ContractInfoModel> Contracts { get; }
}
