using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkCoreLibrary.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event> GetByIdDetailedAsync(int id);
    }
}
