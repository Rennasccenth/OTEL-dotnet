using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nullnes.Options;

namespace Nullnes.Documentation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection serviceCollection, Action<OpenApiDocumentation> openApiDocumentationAction)
    {
        OpenApiDocumentation openApiDocumentation = new();
        
        
        
        serviceCollection.RegisterOption<OpenApiDocumentation>(OpenApiDocumentation.SectionName);

        serviceCollection.AddOpenApiDocument((generatorSettings, provider) =>
        {
            OpenApiDocumentation documentationOptions = provider
                .GetRequiredService<IOptions<OpenApiDocumentation>>().Value;

            generatorSettings.Title = documentationOptions.Title;
            generatorSettings.Description = documentationOptions.Description;
        });

        return serviceCollection;
    }
    
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection serviceCollection)
    {
        serviceCollection.RegisterOption<OpenApiDocumentation>(OpenApiDocumentation.SectionName);

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