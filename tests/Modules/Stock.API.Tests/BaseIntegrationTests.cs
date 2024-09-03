using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Stock.API.Tests.Constants;

namespace Stock.API.Tests;

[Collection(TestCollectionConstants.DefaultSharedCollectionName)]
public class BaseIntegrationTests
{
    private readonly StockAPIWebApplicationFactory _stockApiWebApplicationFactory;

    public BaseIntegrationTests(StockAPIWebApplicationFactory stockApiWebApplicationFactory)
    {
        _stockApiWebApplicationFactory = stockApiWebApplicationFactory;
    }

    [Fact]
    public async Task Test()
    {
        // Arrange
        HttpClient client = _stockApiWebApplicationFactory.CreateClient();

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("weatherforecast");

        // Assert
        using (var _ = new AssertionScope())
        {
            responseMessage.StatusCode.Should()
                .Be(HttpStatusCode.OK);
        }
    }

    [Fact]
    public async Task Test2()
    {
        // Arrange
        var client = _stockApiWebApplicationFactory.CreateClient();

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("weatherforecast");
        WeatherForecast[]? response = await responseMessage.Content.ReadFromJsonAsync<WeatherForecast[]>();

        // Assert
        using var _ = new AssertionScope();

        response.Should().NotBeNullOrEmpty();
    }
}