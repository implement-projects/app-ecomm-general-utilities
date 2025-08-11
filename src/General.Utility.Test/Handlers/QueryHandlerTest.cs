namespace General.Utility.Test.Handlers;

public class QueryHandlerTest
{
    [Fact]
    public async Task QueryAsync_Should_Call_QueryHandler_And_Return_Result()
    {
        // Arrange
        var mockHandler = new Mock<IQueryHandler<TestWithResponseQuery, string>>();
        mockHandler.Setup(h => h.HandleAsync(It.IsAny<TestWithResponseQuery>(), DefaultValues._cancellationToken)).ReturnsAsync(DefaultValues._queryHandlerResponse);

        var services = new ServiceCollection();
        services.AddSingleton(mockHandler.Object);
        var serviceProvider = services.BuildServiceProvider();

        var dispatcher = new QueryDispatcher(serviceProvider);
        var query = new TestWithResponseQuery();

        // Act
        var result = await dispatcher.QueryAsync<TestWithResponseQuery, string>(query);

        // Assert
        Assert.Equal(DefaultValues._queryHandlerResponse, result);
        mockHandler.Verify(h => h.HandleAsync(query, DefaultValues._cancellationToken), Times.Once);
    }
}
