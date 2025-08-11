namespace General.Utility.CQRS.Dispatcher;

public class CommandDispatcher(IServiceProvider provider) : ICommandDispatcher
{
    private readonly IServiceProvider _provider = provider;

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
        where TCommand : ICommand
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var handler = _provider.GetService(handlerType) ?? throw new HandlerNotFoundException("No handler found for command", command.GetType());
        var method = handlerType.GetMethod("HandleAsync");
        await (Task)method?.Invoke(obj: handler, parameters: [command, cancellationToken])!;
    }

    public async Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType) ?? throw new HandlerNotFoundException("No handler found for command", command.GetType());
        var method = handlerType.GetMethod("HandleAsync");
        return await (Task<TResponse>)method?.Invoke(obj: handler, parameters: [command, cancellationToken])!;
    }
}
