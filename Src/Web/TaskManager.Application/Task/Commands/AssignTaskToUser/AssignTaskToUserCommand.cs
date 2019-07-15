﻿namespace TaskManager.Application.Task.Commands.AssignTaskToUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class AssignTaskToUserCommand : IRequest
    {
        public int TaskId { get; set; }

        public string ApplicationUserId { get; set; }

        public class Handler : IRequestHandler<AssignTaskToUserCommand>
        {
            private readonly ITaskManagerDbContext context;

            private readonly UserManager<ApplicationUser> userManager;

            public Handler(ITaskManagerDbContext context, UserManager<ApplicationUser> userManager)
            {
                this.context = context;
                this.userManager = userManager;
            }

            public async Task<Unit> Handle(AssignTaskToUserCommand request, CancellationToken cancellationToken)
            {
                await new AssignTaskToUserValidator().ValidateAndThrowAsync(request);

                var user = await this.context.Users.FindAsync(request.ApplicationUserId)
                    ?? throw new UserNotFoundException();

                if (await this.userManager.IsInRoleAsync(user, "Manager")
                    || await this.userManager.IsInRoleAsync(user, "Developer"))
                {
                  var task = await this.context.Tasks.FindAsync(request.TaskId)
                      ?? throw new EntityNotFoundException();

                  task.ApplicationUserId = user.Id;
                  await this.context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    throw new UserHaveNoPermissionException();
                }

                return Unit.Value;
            }
        }
    }
}