using Xunit;

namespace JLek.POS.IntegrationTests.Infrastructure;

[CollectionDefinition("Reports")]
public sealed class ReportingCollectionFixture : ICollectionFixture<CustomWebApplicationFactory>
{
}