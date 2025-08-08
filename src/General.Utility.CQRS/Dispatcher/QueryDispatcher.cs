namespace General.Utility.CQRS.Dispatcher;

public class QueryDispatcher(IServiceProvider provider) : IQueryDispatcher
{
    private readonly IServiceProvider _provider = provider;

    public async Task<TResponse> DispatchAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default) 
        where TQuery : IQuery<TResponse>
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType) ?? throw new HandlerNotFoundException("No handler found for command", query.GetType());
        var method = handlerType.GetMethod("HandleAsync");
        var result = await (Task<TResponse>)method.Invoke(handler, [query, cancellationToken]);

        return result;

        //if (query == null)
        //{
        //    throw new ArgumentNullException(nameof(query));
        //}

        //// Get the specific query handler from the service provider.
        //var handler = _provider.GetService<IQueryHandler<TQuery, TResponse>>();
        //if (handler == null)
        //{
        //    throw new InvalidOperationException($"No handler found for query {typeof(TQuery).Name}");
        //}

        //return await handler.HandleAsync(query);
    }
}
