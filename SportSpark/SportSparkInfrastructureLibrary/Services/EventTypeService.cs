using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkInfrastructureLibrary.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IBaseRepository<EventType> _eventTypeRepository;
        private readonly IMapper _mapper;
        public EventTypeService(IBaseRepository<EventType> eventTypeRepository, IMapper mapper)
        {
            _eventTypeRepository = eventTypeRepository;
            _mapper = mapper;
        }
        public Task<int> CreateAsync(EventTypeDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventTypeDTO>> GetAllAsync()
        {
            var eventTypes = await _eventTypeRepository.GetAllAsync();

            return _mapper.Map<List<EventTypeDTO>>(eventTypes);
        }

        public Task<EventTypeDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, EventTypeDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
