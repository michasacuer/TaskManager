namespace TaskManager.Application.Task.Commands.EditTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class EditTaskCommand : IRequest
    {
        public ToDoTask Data { get; set; }

        public class Handler : IRequestHandler<EditTaskCommand>
        {
            private readonly ITaskRepository taskRepository;

            public Handler(ITaskRepository taskRepository)
            {
                this.taskRepository = taskRepository;
            }

            public async Task<Unit> Handle(EditTaskCommand request, CancellationToken cancellationToken)
            {
                await new EditTaskCommandValidator().ValidateAndThrowAsync(request);

                var task = await this.taskRepository.GetByIdAsync(request.Data.Id)
                    ?? throw new EntityNotFoundException();

                task.Name = request.Data.Name;
                task.Description = request.Data.Description;

                this.taskRepository.Update(task);
                await this.taskRepository.SaveAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
