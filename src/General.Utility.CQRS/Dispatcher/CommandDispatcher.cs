namespace General.Utility.CQRS.Dispatcher;

public class CommandDispatcher(IServiceProvider provider) : ICommandDispatcher
{
    private readonly IServiceProvider _provider = provider;

    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
        where TCommand : ICommand
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var handler = _provider.GetService(handlerType) ?? throw new HandlerNotFoundException("No handler found for command", command.GetType());
        var method = handlerType.GetMethod("HandleAsync");
        await (Task)method.Invoke(obj: handler, parameters: [command, cancellationToken]);

        //if (command == null)
        //{
        //    throw new ArgumentNullException(nameof(command));
        //}

        //// Get the specific command handler from the service provider.
        //var handler = _provider.GetService<ICommandHandler<TCommand>>();
        //if (handler == null)
        //{
        //    throw new InvalidOperationException($"No handler found for command {typeof(TCommand).Name}");
        //}

        //await handler.HandleAsync(command);
    }
}
