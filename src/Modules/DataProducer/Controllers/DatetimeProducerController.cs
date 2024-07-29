using System.Runtime.CompilerServices;
using Grpc.Net.Client;
using gRPC.Spammer.Contracts;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc.Client;

namespace DataProducer.Controllers;

[ApiController]
[Route("[controller]")]
public class DatetimeProducerController : Controller
{
    [HttpGet]
    public async IAsyncEnumerable<DateTime> Index([FromServices] IConfiguration configuration, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var channel = GrpcChannel.ForAddress(configuration["gRPCServerAddress"]!);
        IDataSpammerService dataSpammerService = channel.CreateGrpcService<IDataSpammerService>();

        await foreach (DataSpammerResponse response in dataSpammerService.GetDataAsync(cancellationToken))
        {
            yield return response.CurrentTimestamp;
        }
    }
}