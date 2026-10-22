using Application.Dispatching.Dispatchers.Handlers;
using Application.Dispatching.Dispatchers.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services.AddSingleton<ICommandHandlersDispatcher, CommandHandlersDispatcher>()
                       .AddSingleton<IQueryHandlersDispatcher, QueryHandlersDispatcher>();
    }
}
