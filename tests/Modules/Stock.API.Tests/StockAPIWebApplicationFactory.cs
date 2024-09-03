using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Stock.API.Tests;

public sealed class StockAPIWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, collection) =>
        {

        });

        base.ConfigureWebHost(builder);
    }
}