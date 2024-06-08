namespace mark.davison.spacetraders.shared.models.dtos.Shared;

public sealed class AccountSummaryDto
{
    public Guid AccountId { get; set; }
    public long Credits { get; set; }
    public string Headquarters { get; set; } = string.Empty;
    public int ShipCount { get; set; }
}
