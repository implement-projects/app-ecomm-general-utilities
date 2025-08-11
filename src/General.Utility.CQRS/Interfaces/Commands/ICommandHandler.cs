namespace General.Utility.CQRS.Interfaces.Commands;

public interface ICommandHandler<in TCommand> 
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
    Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}