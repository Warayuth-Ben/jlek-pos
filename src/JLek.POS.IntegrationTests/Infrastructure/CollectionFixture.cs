using Xunit;

namespace JLek.POS.IntegrationTests.Infrastructure;

[CollectionDefinition("Catalog")]
public sealed class CatalogCollectionFixture : ICollectionFixture<CustomWebApplicationFactory>
{
}