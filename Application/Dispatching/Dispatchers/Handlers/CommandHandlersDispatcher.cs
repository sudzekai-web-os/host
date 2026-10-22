using Application.Dispatching.Handlers.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Dispatching.Dispatchers.Handlers
{
    internal class CommandHandlersDispatcher(IServiceProvider provider) : ICommandHandlersDispatcher
    {
        async Task<TResult> ICommandHandlersDispatcher.ExecuteAsync<TCommand, TResult>(TCommand command)
        {
            var service = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

            return await service.HandleAsync(command);
        }
    }
}
