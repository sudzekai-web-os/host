using Application.Dispatching.Objects.Commands;

namespace Application.Dispatching.Handlers.Commands
{
    internal interface ICommandHandler<TCommand, TResult>
        where TCommand : class, ICommand<TResult>
        where TResult : class
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
