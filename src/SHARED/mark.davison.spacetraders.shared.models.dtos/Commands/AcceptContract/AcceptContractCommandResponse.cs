namespace mark.davison.spacetraders.shared.models.dtos.Commands.AcceptContract;

public sealed class AcceptContractCommandResponse : Response<ContractDto>
{
    public AgentDto? Agent { get; set; }
}
