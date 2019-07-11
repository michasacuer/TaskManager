namespace TaskManager.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;

    public class DatabaseContextFactory
    {
        public static TaskManagerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .EnableSensitiveDataLogging(true)
                                .Options;

            var context = new TaskManagerDbContext(options);

            context.Projects.AddRange(AddProjectsToDatabase());
            context.SaveChanges();

            return context;
        }

        public static void Destroy(TaskManagerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public static Project[] AddProjectsToDatabase()
        {
            int projectsCount = 2;
            var projects = new Project[projectsCount];

            for (int i = 0; i < projectsCount; i++)
            {
                projects[i] = new Project
                {
                    Name = $"Project{i}",
                    Description = $"Description{i}"
                };
            }

            return projects;
        }
    }
}
