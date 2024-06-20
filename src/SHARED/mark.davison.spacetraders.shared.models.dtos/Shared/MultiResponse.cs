namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class MultiResponse
{
    public AgentDto? AgentDto { get; set; }
    public ContractDto? ContractDto { get; set; }
    public ShipResponse? ShipResponse { get; set; }
}
