﻿namespace TaskManager.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface ITaskManagerDbContext
    {
        DbSet<Domain.Entity.Project> Projects { get; set; }

        DbSet<Domain.Entity.Task> Tasks { get; set; }

        DbSet<Domain.Entity.EndedTask> EndedTasks { get; set; }

        DbSet<Domain.Entity.Notification> Notifications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}