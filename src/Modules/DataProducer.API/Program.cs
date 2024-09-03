using DataProducer.API.Extensions;
using Logging;
using Monitoring;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder
    .ConfigureSerilog()
    .ConfigureOpenTelemetry();

IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services
    .AddEndpointsApiExplorer()
    .ConfigureSwaggerDoc()
    .AddControllers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(settings =>
    {
        settings.Path = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference(options =>
    {
        options.EndpointPathPrefix = "/docs";
        options.DarkMode = true;
        options.ShowSideBar = true;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();