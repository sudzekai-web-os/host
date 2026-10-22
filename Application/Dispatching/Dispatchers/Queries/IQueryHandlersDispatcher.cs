using Application.Dispatching.Objects.Queries;

namespace Application.Dispatching.Dispatchers.Queries
{
    public interface IQueryHandlersDispatcher
    {
        Task QueryAsync<TQuery>(TQuery query)
            where TQuery : class, IQuery;
    }
}
