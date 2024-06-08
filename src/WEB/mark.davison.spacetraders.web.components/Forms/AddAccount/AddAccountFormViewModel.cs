namespace mark.davison.spacetraders.web.components.Forms.AddAccount;

public sealed class AddAccountFormViewModel : IFormViewModel
{
    public bool AddExisting { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public Guid? FactionSymbol { get; set; }

    private bool ExistingValid => AddExisting && !string.IsNullOrEmpty(Token);
    private bool NewValid => !AddExisting && !string.IsNullOrEmpty(Identifier) && FactionSymbol != null && FactionSymbol != Guid.Empty;

    public bool Valid => ExistingValid || NewValid;

    // TODO: Better method or maybe make a look up???
    public List<IDropdownItem> FactionSymbols { get; } =
        [
            new DropdownItem{ Id = Guid.NewGuid(), Name = "Cosmic" },
            new DropdownItem{ Id = Guid.NewGuid(), Name = "Void" },
            new DropdownItem{ Id = Guid.NewGuid(), Name = "Galactic" }
        ];
}
