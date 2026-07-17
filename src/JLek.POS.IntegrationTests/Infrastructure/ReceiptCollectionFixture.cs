using Xunit;

namespace JLek.POS.IntegrationTests.Infrastructure;

[CollectionDefinition("Receipts")]
public sealed class ReceiptCollectionFixture : ICollectionFixture<CustomWebApplicationFactory>
{
}