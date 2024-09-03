using Stock.API.Tests.Constants;

namespace Stock.API.Tests;

[CollectionDefinition(TestCollectionConstants.DefaultSharedCollectionName)]
public sealed class SharedTestCollectionFixture : ICollectionFixture<StockAPIWebApplicationFactory>;
