using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace DataProducer.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSwaggerDoc(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddOptions<OpenApiDocumentation>()
            .BindConfiguration(OpenApiDocumentation.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        serviceCollection.AddOpenApiDocument((generatorSettings, provider) =>
        {
            OpenApiDocumentation documentationOptions = provider
                .GetRequiredService<IOptions<OpenApiDocumentation>>().Value;

            generatorSettings.Title = documentationOptions.Title;
            generatorSettings.Description = documentationOptions.Description;
        });

        return serviceCollection;
    }
}

internal sealed class OpenApiDocumentation
{
    public const string SectionName = nameof(OpenApiDocumentation);

    [Required]
    public required string Title { get; init; }

    [Required]
    public required string Description { get; init; }
}