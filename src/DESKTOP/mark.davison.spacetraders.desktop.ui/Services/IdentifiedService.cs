namespace mark.davison.spacetraders.desktop.ui.Services;

internal abstract class IdentifiedService
{
    private readonly IClientHttpRepository _clientHttpRepository;
    protected readonly IAccountService _accountService;

    public IdentifiedService(
        IClientHttpRepository clientHttpRepository,
        IAccountService accountService)
    {
        _clientHttpRepository = clientHttpRepository;
        _accountService = accountService;
    }

    protected async Task<TResponse> Get<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : class, IQuery<TRequest, TResponse>
        where TResponse : Response, new()
    {
        if (_accountService.HasActiveAccount)
        {
            if (request is IdentifiedQueryRequest<TRequest, TResponse> identifiedRequest)
            {
                identifiedRequest.Identifier = _accountService.GetActiveAccount().Identifier;
            }
        }

        return await _clientHttpRepository.Get<TResponse, TRequest>(request, cancellationToken);
    }

    protected async Task<TResponse> Post<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : class, ICommand<TRequest, TResponse>
        where TResponse : Response, new()
    {
        if (_accountService.HasActiveAccount)
        {
            if (request is IdentifiedCommandRequest<TRequest, TResponse> identifiedRequest)
            {
                identifiedRequest.Identifier = _accountService.GetActiveAccount().Identifier;
            }
        }

        return await _clientHttpRepository.Post<TResponse, TRequest>(request, cancellationToken);
    }
}
