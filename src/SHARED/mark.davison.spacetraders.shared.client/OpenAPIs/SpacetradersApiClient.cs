namespace Spacetraders.Api.Client;

public partial class SpacetradersApiClient
{
    protected partial async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url)
    {
        request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Token}");

        DateTimeOffset waitStartTime = DateTime.UtcNow;

        await _rateLimitSemaphore.WaitAsync();

        DateTimeOffset waitEndTime = DateTime.UtcNow;

        var waitMilliseconds = (waitEndTime - waitStartTime).TotalMilliseconds;

        if (waitMilliseconds <= 1)
        {
            var waitMicroseconds = (waitEndTime - waitStartTime).TotalMicroseconds;

            _logger.LogInformation("Waited {0:N0}μs for Spacetraders api", waitMicroseconds);
        }
        else
        {
            if (waitMilliseconds > 250)
            {
                _logger.LogWarning("Waited {0:N0}ms for Spacetraders api", waitMilliseconds);
            }
            else
            {
                _logger.LogInformation("Waited {0:N0}ms for Spacetraders api", waitMilliseconds);
            }
        }
    }

    partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response)
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            _rateLimitSemaphore.Release();
        });
    }

    static partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
    {
        settings.Converters.Add(new JsonStringEnumConverter());
    }

}
