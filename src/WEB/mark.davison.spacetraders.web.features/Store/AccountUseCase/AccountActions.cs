namespace mark.davison.spacetraders.web.features.Store.AccountUseCase;

public sealed class FetchAccountsAction : BaseAction
{
}

public sealed class FetchAccountsActionResponse : BaseActionResponse<List<AccountDto>>
{
}

public sealed class AddAccountAction : BaseAction
{
    public bool AddExisting { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string FactionSymbol { get; set; } = string.Empty;
}

public sealed class AddAccountActionResponse : BaseActionResponse<AccountDto>
{
}

public sealed class FetchAccountSummaryAction : BaseAction
{
    public Guid AccountId { get; set; }
}

public sealed class FetchAccountSummaryActionResponse : BaseActionResponse<AccountSummaryDto>
{

}

public sealed class UpdateCreditsAction : BaseAction
{
    public Guid AccountId { get; set; }
    public long Credits { get; set; }
}