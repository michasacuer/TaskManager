namespace TaskManager.Application.Comment.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddCommentCommand : IRequest
    {
        public string Content { get; set; }

        public class Hanlder : IRequestHandler<AddCommentCommand>
        {
            public Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
