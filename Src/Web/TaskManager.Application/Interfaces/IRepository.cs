namespace TaskManager.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<T>
        where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task AddAsync(T item);

        void Update(T item);

        void Delete(T item);

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
