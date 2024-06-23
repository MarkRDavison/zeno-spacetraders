using mark.davison.spacetraders.shared.models.dtos.Commands.RegisterAgent;

namespace mark.davison.spacetraders.desktop.ui.Dialogs;

public partial class RegisterAgentDialogViewModel : ObservableObject, IFormViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Valid))]
    private string _identifier = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private ComboBoxItem? _factionItem;

    public bool Valid =>
        !string.IsNullOrEmpty(Identifier) &&
        3 <= Identifier.Length && Identifier.Length <= 14 &&
        FactionItem is not null;
}

public sealed class RegisterAgentDialogFormSubmission : IFormSubmission<RegisterAgentDialogViewModel>
{
    private readonly IClientHttpRepository _clientHttpRepository;

    public RegisterAgentDialogFormSubmission(IClientHttpRepository clientHttpRepository)
    {
        _clientHttpRepository = clientHttpRepository;
    }

    public async Task<Response> Primary(RegisterAgentDialogViewModel formViewModel)
    {
        var faction = formViewModel.FactionItem?.Content?.ToString()?.ToUpperInvariant();

        var response = new Response<AgentDto>();

        if (string.IsNullOrEmpty(faction))
        {
            response.Errors.Add("Must specify a faction");
        }
        if (string.IsNullOrEmpty(formViewModel.Identifier))
        {
            response.Errors.Add("Must specify an identifier");
        }

        if (!response.Success)
        {
            return response;
        }

        var commandResponse = await _clientHttpRepository.Post<RegisterAgentCommandResponse, RegisterAgentCommandRequest>(new()
        {
            Identifier = formViewModel.Identifier,
            Email = formViewModel.Email,
            Faction = faction ?? "COSMIC"
        }, CancellationToken.None);

        response = new Response<AgentDto>
        {
            Errors = commandResponse.Errors,
            Warnings = commandResponse.Warnings,
            Value = commandResponse.Value
        };

        return response;
    }
}