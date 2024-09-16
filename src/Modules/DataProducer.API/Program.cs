using Nullnes.Documentation;
using Nullnes.Logging;
using Nullnes.Monitoring;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder
    .ConfigureSerilog()
    .ConfigureOpenTelemetry();

IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services
    .AddEndpointsApiExplorer()
    .AddOpenApiDocumentation()
    .AddControllers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApiDocumentation();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();