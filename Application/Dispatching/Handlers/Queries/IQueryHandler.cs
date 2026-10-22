namespace Application.Dispatching.Handlers.Queries
{
    internal interface IQueryHandler<TQuery>
    {
        Task HandleAsync(TQuery query);
    }
}
