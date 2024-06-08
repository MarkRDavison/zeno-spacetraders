namespace mark.davison.spacetraders.shared.models.dtos.Commands.FetchContracts;

public class FetchContractsCommandResponse : Response<List<ContractDto>>
{
    public MetaInfo Meta { get; set; } = new();
}
