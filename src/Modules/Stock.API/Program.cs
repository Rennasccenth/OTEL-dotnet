using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nullnes.Logging;
using Nullnes.Mediation;
using Nullnes.Monitoring;
using Stock.API;
using Stock.API.ExceptionHandlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureSerilog()
    .ConfigureOpenTelemetry();

builder.Services.AddMediation(mediationConfiguration =>
{
    mediationConfiguration
        .EnableValidation()
        .RequireRequestValidator()
        .UsingAssemblies(Assembly.GetExecutingAssembly());
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseHttpsRedirection();

app.MapGet("/weatherforecast2", async (
        [FromServices] IMediator mediator) =>
    {
        var forecast = await mediator.Send(new GetWeatherForecastRequest());
        
        return forecast;
    })
    .WithName("GetWeatherForecast2");

app.Run();

namespace Stock.API
{
    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
    
    public class GetWeatherForecastRequest : IRequest<WeatherForecast[]>
    {
    }

    internal sealed class GetWeatherForecastRequestValidator : AbstractValidator<GetWeatherForecastRequest>
    {
        internal GetWeatherForecastRequestValidator()
        {
            
        }
    }

    internal sealed class GetWeatherForecastRequestHandler : IRequestHandler<GetWeatherForecastRequest, WeatherForecast[]>
    {
        public async Task<WeatherForecast[]> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            await Task.Delay(0, cancellationToken);
            
            return Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                .ToArray();
        }
    }

    public partial class Program{}
}