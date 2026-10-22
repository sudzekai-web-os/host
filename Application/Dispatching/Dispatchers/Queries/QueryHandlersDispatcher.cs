using Application.Dispatching.Handlers.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Dispatching.Dispatchers.Queries
{
    internal class QueryHandlersDispatcher(IServiceProvider provider) : IQueryHandlersDispatcher
    {
        async Task IQueryHandlersDispatcher.QueryAsync<TQuery>(TQuery query)
        {
            var service = provider.GetRequiredService<IQueryHandler<TQuery>>();

            await service.HandleAsync(query);
        }
    }
}
