namespace TaskManager.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
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

            DataSeeding.Run(context);
            context.SaveChanges();

            return context;
        }

        public static void Destroy(TaskManagerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
