namespace TaskManager.Persistence
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class TaskManagerDbContext : IdentityDbContext<ApplicationUser>, ITaskManagerDbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ToDoTask> Tasks { get; set; }

        public DbSet<EndedTask> EndedTasks { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
        }
    }
}
