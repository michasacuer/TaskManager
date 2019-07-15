namespace TaskManager.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Domain.Entity;
    using TaskManager.Domain.Enum;
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

            context.Users.AddRange(AddUsersToDatabase());
            context.Projects.AddRange(AddProjectsToDatabase());
            context.Tasks.AddRange(AddTasksToDatabase());

            context.SaveChanges();

            return context;
        }

        public static void Destroy(TaskManagerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public static ApplicationUser[] AddUsersToDatabase()
        {
            int usersCount = 2;
            var users = new ApplicationUser[usersCount];

            for (int i = 0; i < usersCount; i++)
            {
                users[i] = new ApplicationUser
                {
                    FirstName = $"First{i}",
                    LastName = $"Name{i}"
                };
            }

            return users;
        }

        public static Project[] AddProjectsToDatabase()
        {
            int projectsCount = 6;
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

        private static Task[] AddTasksToDatabase()
        {
            int tasksCount = 5;
            var tasks = new Task[tasksCount];

            for (int i = 0; i < tasksCount; i++)
            {
                if (i % 2 == 0)
                {
                    tasks[i] = new Task
                    {
                        Name = $"Task{i}",
                        Description = $"Desc{i}",
                        Priority = Priority.Low,
                        ProjectId = 6,
                        StoryPoints = 20
                    };
                }
                else
                {
                    tasks[i] = new Task
                    {
                        Name = $"Task{i}",
                        Description = $"Desc{i}",
                        Priority = Priority.High,
                        ProjectId = 6
                    };
                }
            }

            return tasks;
        }
    }
}
