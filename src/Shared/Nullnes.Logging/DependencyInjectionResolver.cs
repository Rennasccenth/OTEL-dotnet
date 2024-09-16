using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace Nullnes.Logging;

public static class DependencyInjectionResolver
{
    public static IHostApplicationBuilder ConfigureSerilog(this IHostApplicationBuilder hostApplicationBuilder)
    {
        hostApplicationBuilder.Logging.ClearProviders();

        hostApplicationBuilder.Services.AddSerilog(loggerConfiguration =>
            {
                loggerConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithCorrelationId()
                    .WriteTo.Async(writer => writer.Console());
            },
            preserveStaticLogger: true,
            writeToProviders: true);

        return hostApplicationBuilder;
    } 
}