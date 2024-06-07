namespace mark.davison.spacetraders.core.Api;

public class AuthenticationContext : IAuthenticationContext
{
    public string Token { get; set; } = string.Empty;
}
