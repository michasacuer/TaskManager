namespace TaskManager.Contracts.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Entity.Base;

    public interface IBaseContract<T>
        where T : BaseEntity<int>
    {
        Task<T> AddAsync(T data);

        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<bool> EditAsync(T data);
    }
}
