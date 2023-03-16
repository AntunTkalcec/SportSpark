using AutoMapper;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkInfrastructureLibrary.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<EventDTO> GetByIdAsync(int id)
        {
            var ev = await _eventRepository.GetByIdDetailedAsync(id);

            return _mapper.Map<EventDTO>(ev);
        }
    }
}
