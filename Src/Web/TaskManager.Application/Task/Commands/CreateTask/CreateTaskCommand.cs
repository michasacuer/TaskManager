namespace TaskManager.Application.Task.Commands.CreateTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Enum;

    public class CreateTaskCommand : IRequest
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public int? StoryPoints { get; set; }

        public class Handler : IRequestHandler<CreateTaskCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                await new CreateTaskCommandValidator().ValidateAndThrowAsync(request);

                var project = await this.context.Projects.FindAsync(request.ProjectId)
                    ?? throw new EntityNotFoundException();

                var task = new Domain.Entity.Task
                {
                    Name = request.Name,
                    Description = request.Description,
                    Priority = request.Priority,
                    ProjectId = request.ProjectId
                };

                this.context.Tasks.Add(task);
                await this.context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
