namespace Integration.Tests;

public class IntegrationTestsFixture : IClassFixture<ApiWebApplicationFactory>
{
    internal readonly ApiWebApplicationFactory AppFactory;
    
    private protected IntegrationTestsFixture(ApiWebApplicationFactory appFactory) => AppFactory = appFactory;
}