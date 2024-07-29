using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services
    .AddEndpointsApiExplorer()
    .AddOpenApiDocument(options =>
    {
        options.DocumentName = "v1";
        options.Title = "Data Producer API";
        options.Version = "v1";
    }).AddControllers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(settings => { settings.Path = "/openapi/v1.json"; });
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