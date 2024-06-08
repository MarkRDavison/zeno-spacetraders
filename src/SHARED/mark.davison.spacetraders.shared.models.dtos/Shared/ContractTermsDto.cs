namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ContractTermsDto
{
    public ContractPaymentDto Payment { get; set; } = new();
}
