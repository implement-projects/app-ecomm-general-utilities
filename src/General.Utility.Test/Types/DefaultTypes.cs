namespace General.Utility.Test.Types;

// Command
public class TestWithoutResponseCommand : ICommand { }

public class TestWithResponseCommand : ICommand<string> 
{
    public string? TestResponse { get; set; }
}

// Command Handlers
public class TestWithoutResponseCommandHandler : ICommandHandler<TestWithoutResponseCommand>
{
    public Task HandleAsync(TestWithoutResponseCommand command, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}

public class TestWithResponseCommandHandler : ICommandHandler<TestWithResponseCommand, string>
{
    public Task<string> HandleAsync(TestWithResponseCommand command, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(DefaultValues._commandHandlerResponse);
    }
}

// Query
public class TestWithResponseQuery : IQuery<string> { }

// Query Handler
public class TestWithResponseQueryHandler : IQueryHandler<TestWithResponseQuery, string>
{
    public Task<string> HandleAsync(TestWithResponseQuery query, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(DefaultValues._queryHandlerResponse);
    }
}