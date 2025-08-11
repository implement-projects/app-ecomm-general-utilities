namespace General.Utility.CQRS.Interfaces.Commands;

public interface ICommand { }

public interface ICommand<out TResponse>
    where TResponse : notnull
{ }
