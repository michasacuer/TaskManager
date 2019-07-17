namespace TaskManager.Application.Task.Commands.EndTaskByUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class EndTaskByUserCommand : IRequest
    {
        public string ApplicationUserId { get; set; }

        public int TaskId { get; set; }

        public class Handler : IRequestHandler<EndTaskByUserCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(EndTaskByUserCommand request, CancellationToken cancellationToken)
            {
                await new EndTaskByUserCommandValidator().ValidateAndThrowAsync(request);

                var task = await this.context.Tasks.FindAsync(request.TaskId)
                    ?? throw new EntityNotFoundException();

                await new EndTaskByUserCommandValidator(task).ValidateAndThrowAsync(request);

                var endedTask = new EndedTask
                {
                    Name = task.Name,
                    Description = task.Description,
                    ProjectId = task.ProjectId,
                    ApplicationUserId = task.ApplicationUserId,
                    Priority = task.Priority,
                    StartTime = task.StartTime,
                    EndTime = DateTime.UtcNow
                };

                this.context.EndedTasks.Add(endedTask);
                this.context.Tasks.Remove(task);
                await this.context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
