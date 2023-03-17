using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkInfrastructureLibrary.Services
{
    public class EventService : IEventService
    {
        private readonly IBaseRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IBaseRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<EventDTO>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync(e => e.User, e => e.EventType, e => e.RepeatType);

            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<EventDTO> GetByIdAsync(int id)
        {
            var ev = await _eventRepository.GetByIdAsync(id, e => e.User, e => e.EventType, e => e.RepeatType);

            return _mapper.Map<EventDTO>(ev);
        }
    }
}
