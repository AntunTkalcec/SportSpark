using SportSparkCoreLibrary.Entities;

namespace SportSparkCoreLibrary.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync(int id);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
