namespace General.Utility.CQRS.Dispatcher;

public interface IQueryDispatcher    
{
    Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResponse>
        where TResponse : notnull;
}
