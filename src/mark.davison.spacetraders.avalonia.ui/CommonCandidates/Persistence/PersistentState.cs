namespace mark.davison.spacetraders.avalonia.ui.CommonCandidates.Persistence;

public sealed class PersistentState : IPersistentState
{
    private readonly List<PersistedAccount> _accounts = [];
    private readonly ILogger<PersistentState> _logger;
    private readonly string _saveLocation;
    private readonly JsonSerializerOptions _options;
    private readonly TaskCompletionSource _tcs;
    private const string SaveFolderName = "zenospacetrader";

    public PersistentState(ILogger<PersistentState> logger)
    {
        _logger = logger;
        _saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SaveFolderName);
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
        _tcs = new();

        if (!Directory.Exists(_saveLocation))
        {
            Directory.CreateDirectory(_saveLocation);
        }
    }

    public List<PersistedAccount> GetAccounts() => _accounts;
    public async Task<List<PersistedAccount>> LoadAccountsAsync(CancellationToken cancellationToken)
    {
        var loadedAccounts = new List<PersistedAccount>();

        var existing = Directory.EnumerateFiles(_saveLocation, "*.json");

        foreach (var file in existing)
        {
            try
            {
                var contents = await File.ReadAllTextAsync(file, cancellationToken);

                var account = JsonSerializer.Deserialize<PersistedAccount>(contents, _options);

                if (account != null)
                {
                    loadedAccounts.Add(account);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured trying to load the save game at {0}", file);
                throw;
            }
        }

        _accounts.Clear();
        _accounts.AddRange(loadedAccounts);

        _tcs.SetResult();

        return _accounts;
    }

    public async Task AddAccountAsync(string token, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var identifier = jwt.Payload["identifier"] as string;
        var version = jwt.Payload["version"] as string;
        var reset_date = jwt.Payload["reset_date"] as string;

        if (string.IsNullOrEmpty(identifier) ||
            string.IsNullOrEmpty(version) ||
            !DateOnly.TryParse(reset_date, out var resetDate))
        {
            throw new InvalidOperationException("Invalid JWT token");
        }

        if (_accounts.Any(_ => _.Identifier == identifier))
        {
            throw new InvalidOperationException("Duplicate account");
        }

        _accounts.Add(new PersistedAccount
        {
            Identifier = identifier,
            ResetDate = resetDate,
            Token = token,
            Version = version
        });

        await SaveAccountsAsync(cancellationToken);
    }

    public async Task SaveAccountsAsync(CancellationToken cancellationToken)
    {
        foreach (var account in _accounts)
        {
            var actualFileName = "account_" + account.Identifier + ".json";
            var tempFileName = "temp_account_" + account.Identifier + ".json";

            await File.WriteAllTextAsync(
                Path.Combine(_saveLocation, tempFileName),
                JsonSerializer.Serialize(account, _options),
                cancellationToken);

            File.Move(Path.Combine(_saveLocation, tempFileName), Path.Combine(_saveLocation, actualFileName), true);
        }
    }

    public void Reset(CancellationToken cancellationToken)
    {
        Directory.Delete(_saveLocation, true);
        Directory.CreateDirectory(_saveLocation);
    }

    public async Task AwaitAccountLoadingAsync()
    {
        await _tcs.Task;
    }
}
