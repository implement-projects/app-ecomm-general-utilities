namespace General.Utility.CQRS.Dispatcher;

public class QueryDispatcher(IServiceProvider provider)
    : IQueryDispatcher
{
    private readonly IServiceProvider _provider = provider;

    public async Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType) ?? throw new HandlerNotFoundException("No handler found for query", query.GetType());
        var method = handlerType.GetMethod("HandleAsync");
        return await (Task<TResponse>)method?.Invoke(handler, [query, cancellationToken])!;
    }
}