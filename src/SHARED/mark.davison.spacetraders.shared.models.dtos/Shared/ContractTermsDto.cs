namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ContractTermsDto
{
    public DateTimeOffset Deadline { get; set; }
    public ContractPaymentDto Payment { get; set; } = new();
    public List<ContractDeliverGoodDto> ContractDeliverGoods { get; set; } = [];
}
