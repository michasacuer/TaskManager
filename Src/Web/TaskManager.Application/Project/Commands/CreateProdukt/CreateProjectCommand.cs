namespace TaskManager.Application.Commands.CreateProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;

    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateProjectCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                new CreateProjectCommandValidator().ValidateAndThrow(request);

                var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Name.Equals(request.Name));
                if (project != null)
                {
                    throw new EntityAlreadyExistsException();
                }

                this.context.Projects.Add(new Domain.Entity.Project
                {
                    Name = request.Name,
                    Description = request.Description
                });

                await this.context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
