﻿namespace TaskManager.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface ITaskManagerDbContext
    {
        DbSet<Domain.Entity.Project> Projects { get; set; }

        DbSet<Domain.Entity.Task> Tasks { get; set; }

        DbSet<Domain.Entity.EndedTask> EndedTasks { get; set; }

        DbSet<Domain.Entity.Notification> Notifications { get; set; }

        DbSet<Domain.Entity.ApplicationUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
