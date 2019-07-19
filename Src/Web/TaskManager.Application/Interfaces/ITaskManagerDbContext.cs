namespace TaskManager.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using TaskManager.Domain.Entity;

    public interface ITaskManagerDbContext
    {
        DbSet<Project> Projects { get; set; }

        DbSet<ToDoTask> Tasks { get; set; }

        DbSet<EndedTask> EndedTasks { get; set; }

        DbSet<Notification> Notifications { get; set; }

        DbSet<ApplicationUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
