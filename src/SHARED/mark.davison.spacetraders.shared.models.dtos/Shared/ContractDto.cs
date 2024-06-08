namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class ContractDto
{
    public string Id { get; set; } = string.Empty;
    public string FactionSymbol { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool Accepted { get; set; } = false;
    public bool Fulfilled { get; set; } = false;
    public ContractTermsDto Terms { get; set; } = new();
    public DateTimeOffset DeadlineToAccept { get; set; }
}
