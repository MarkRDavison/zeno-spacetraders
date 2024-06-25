namespace mark.davison.spacetraders.shared.models.dtos;

public static class ValidationMessages
{
    public const string ERROR_SAVING = nameof(ERROR_SAVING);
    public const string ERROR_DELETING = nameof(ERROR_DELETING);
    public const string FAILED_TO_FIND_ENTITY = nameof(FAILED_TO_FIND_ENTITY);
    public const string INVALID_PROPERTY = nameof(INVALID_PROPERTY);
    public const string INSUFFICIENT_CREDITS = nameof(INSUFFICIENT_CREDITS);
    public const string DUPLICATE_ENTITY = nameof(DUPLICATE_ENTITY);
    public const string NO_ITEMS = nameof(NO_ITEMS);
    public const string BAD_REQUEST = nameof(BAD_REQUEST);

    public static string FormatMessageParameters(string message, params string[] parameters)
    {
        if (parameters.Length == 0)
        {
            return message;
        }
        var parametersSegment = string.Join('&', parameters);
        return message + '&' + parametersSegment;
    }

    public static T CreateErrorResponse<T>(string message, params string[] parameters) where T : Response, new()
    {
        return new T()
        {
            Errors = [
                FormatMessageParameters(message, parameters)
            ]
        };
    }
}
