namespace TaskManager.Application.Base
{
    using MediatR;
    
    public abstract class QueryBase<T> : UserContext, IRequest<T>
    {
    }
}
