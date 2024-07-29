using System.ServiceModel;
using ProtoBuf;

namespace gRPC.Spammer.Contracts;

[ProtoContract]
public sealed class DataSpammerResponse
{
    [ProtoMember(1)]
    public required DateTime CurrentTimestamp { get; init; }
}

[ServiceContract]
public interface IDataSpammerService
{
    [OperationContract]
    public IAsyncEnumerable<DataSpammerResponse> GetDataAsync(CancellationToken cancellationToken);
}