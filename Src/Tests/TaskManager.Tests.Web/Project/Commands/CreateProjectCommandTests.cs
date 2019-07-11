﻿namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class CreateProjectCommandTests
    {
        private readonly TaskManagerDbContext context;

        public CreateProjectCommandTests()
        {
            this.context = DatabaseContextFactory.Create();
        }

        [Fact]
        public async Task CreateProjectShouldAddProjectToDatabase()
        {
            var command = new CreateProjectCommand
            {
                Name = "Project",
                Description = "Description"
            };

            var commandHandler = new CreateProjectCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None);

            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Name.Equals(command.Name));

            project.ShouldNotBeNull();
            project.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task CreateProjectWithoutDescriptionShouldAddProjectToDatabase()
        {
            var command = new CreateProjectCommand
            {
                Name = "Project2",
            };

            var commandHandler = new CreateProjectCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None);

            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Name.Equals(command.Name));

            project.ShouldNotBeNull();
            project.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task CreateProjectWithEmptyNameShouldThrowException()
        {
            var command = new CreateProjectCommand
            {
                Description = "Description of project without name"
            };

            var commandHandler = new CreateProjectCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task CreateProjectWithSameNameAsAnotherShouldThrowException()
        {
            var command = new CreateProjectCommand
            {
                Name = "Project0",
                Description = "Description"
            };

            var commandHandler = new CreateProjectCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityAlreadyExistsException>();
        }
    }
}