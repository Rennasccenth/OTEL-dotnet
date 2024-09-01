using DateTimeSpammer.gRPC.Server.Services;
using Logging;
using Monitoring;
using ProtoBuf.Grpc.Server;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder
    .ConfigureSerilog()
    .ConfigureOpenTelemetry();

IServiceCollection services = builder.Services;

services.AddSingleton(TimeProvider.System);

services.AddCodeFirstGrpc(options =>
{
    options.IgnoreUnknownServices = true;
    options.EnableDetailedErrors = true;
});

WebApplication app = builder.Build();

app.MapGrpcService<DataSpammerGrpcService>();
app.Run();