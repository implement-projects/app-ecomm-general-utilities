namespace General.Utility.Test.Handlers;

public class CommandHandlerTest
{
    [Fact]
    public async Task SendAsync_Should_Invoke_CommandHandler()
    {
        // Arrange
        var mockHandler = new Mock<ICommandHandler<TestWithoutResponseCommand>>();
        mockHandler.Setup(h => h.HandleAsync(It.IsAny<TestWithoutResponseCommand>(), DefaultValues._cancellationToken));

        var services = new ServiceCollection();
        services.AddSingleton(mockHandler.Object);

        var serviceProvider = services.BuildServiceProvider();

        var dispatcher = new CommandDispatcher(serviceProvider);
        var command = new TestWithoutResponseCommand();

        // Act
        await dispatcher.SendAsync(command);

        // Assert
        mockHandler.Verify(h => h.HandleAsync(command, DefaultValues._cancellationToken), times: Times.Once);
    }

    [Fact]
    public async Task SendAsync_Should_Call_CommandHandler_And_Return_Result()
    {
        // Arrange
        var mockHandler = new Mock<ICommandHandler<TestWithResponseCommand, string>>();
        mockHandler.Setup(h => h.HandleAsync(It.IsAny<TestWithResponseCommand>(), DefaultValues._cancellationToken)).ReturnsAsync(DefaultValues._commandHandlerResponse);

        var services = new ServiceCollection();
        services.AddSingleton(mockHandler.Object);

        var serviceProvider = services.BuildServiceProvider();

        var dispatcher = new CommandDispatcher(serviceProvider);
        var command = new TestWithResponseCommand();

        // Act
        var response = await dispatcher.SendAsync<TestWithResponseCommand, string>(command);

        // Assert
        Assert.Equal(DefaultValues._commandHandlerResponse, response);
        mockHandler.Verify(h => h.HandleAsync(command, DefaultValues._cancellationToken), times: Times.Once);
    }
}
