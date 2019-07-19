namespace TaskManager.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
        where T : class
    {
        Task<T> GetById(int id);

        Task<List<T>> GetAll();

        Task Add(T item);

        Task Update(T item);

        Task Delete(T item);
    }
}
