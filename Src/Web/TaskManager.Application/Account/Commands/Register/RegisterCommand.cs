﻿namespace TaskManager.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Enum;

    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public class Handler : IRequestHandler<RegisterCommand>
        {
            private readonly IApplicationUserService applicationUserService;

            public Handler(IApplicationUserService applicationUserService)
            {
                this.applicationUserService = applicationUserService;
            }

            public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                new RegisterCommandValidator().ValidateAndThrow(request);

                await this.applicationUserService.RegisterAsync(request);

                return Unit.Value;
            }
        }
    }
}
