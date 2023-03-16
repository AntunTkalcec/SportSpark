using SportSparkCoreSharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IEventService
    {
        Task<EventDTO> GetByIdAsync(int id);
    }
}
