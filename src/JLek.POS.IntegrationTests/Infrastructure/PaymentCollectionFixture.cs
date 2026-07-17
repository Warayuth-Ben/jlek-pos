using Xunit;

namespace JLek.POS.IntegrationTests.Infrastructure;

[CollectionDefinition("Payments")]
public sealed class PaymentCollectionFixture : ICollectionFixture<CustomWebApplicationFactory>
{
}