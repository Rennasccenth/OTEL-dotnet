using System.Runtime.CompilerServices;
using gRPC.Spammer.Contracts;

namespace DateTimeSpammer.gRPC.Server.Services;

internal sealed class DataSpammerGrpcService : IDataSpammerService
{
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<DataSpammerGrpcService> _logger;

    public DataSpammerGrpcService(TimeProvider timeProvider, ILogger<DataSpammerGrpcService> logger)
    {
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public IAsyncEnumerable<DataSpammerResponse> GetDataAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return GenerateDataAsync(cancellationToken);
        }
        catch (TaskCanceledException)
        {
            _logger.LogWarning("Operation was canceled");
        }

        throw new InvalidOperationException("Can't reach here.");
    }

    private async IAsyncEnumerable<DataSpammerResponse> GenerateDataAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested is false)
        {
            yield return new DataSpammerResponse
            {
                CurrentTimestamp = _timeProvider.GetUtcNow().DateTime
            };

            await Task.Delay(1000, cancellationToken);
        }
    }
}