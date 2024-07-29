using DateTimeSpammer.gRPC.Server.Services;
using ProtoBuf.Grpc.Server;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
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