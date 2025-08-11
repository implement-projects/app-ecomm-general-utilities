namespace General.Utility.CQRS.Interfaces.Queries;

public interface IQueryHandler<in TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
    Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
