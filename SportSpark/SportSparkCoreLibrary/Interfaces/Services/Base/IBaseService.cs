using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreLibrary.Interfaces.Services.Base
{
    public interface IBaseService<T> where T : BaseDTO
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
    }
}
