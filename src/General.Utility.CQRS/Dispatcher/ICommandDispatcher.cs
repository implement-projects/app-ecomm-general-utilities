namespace General.Utility.CQRS.Dispatcher;

public interface ICommandDispatcher    
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
        where TCommand : ICommand;

    Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResponse>
        where TResponse : notnull;
}