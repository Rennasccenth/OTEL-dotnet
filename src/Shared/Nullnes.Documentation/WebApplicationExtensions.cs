using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace Nullnes.Documentation;

public static class WebApplicationExtensions
{
    public static WebApplication UseOpenApiDocumentation(this WebApplication webApplication,
        ScalarTheme scalarTheme = ScalarTheme.Kepler,
        bool enableDarkMode = true)
    {
        // This path is the default path for .NET 9 generated OpenAPIDocument.
        // Note: {documentName} is a placeholder that is replaced by API versioning tags.
        const string openApiDocumentPath = "/openapi/{documentName}.json";

        webApplication.UseOpenApi(settings =>
        {
            settings.Path = openApiDocumentPath;
        });
        webApplication.MapScalarApiReference(options =>
        {
            options
                .WithOpenApiRoutePattern(openApiDocumentPath)
                .WithEndpointPrefix("/docs/{documentName}")
                .WithTheme(scalarTheme)
                .WithDarkMode(enableDarkMode)
                .WithForceThemeMode(enableDarkMode ? ThemeMode.Dark : ThemeMode.Light);
        });

        return webApplication;
    }
}