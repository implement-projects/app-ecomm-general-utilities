namespace General.Utility.CQRS.Interfaces.Queries;

public interface IQuery<out TResponse>
    where TResponse : notnull
{ }
