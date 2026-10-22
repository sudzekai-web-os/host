using Application.Dispatching.Objects.Commands;

namespace Application.Dispatching.Dispatchers.Handlers
{
    public interface ICommandHandlersDispatcher
    {
        Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command)
            where TCommand : class, ICommand<TResult>
            where TResult : class;
    }
}
