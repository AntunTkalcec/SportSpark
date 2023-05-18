using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkInfrastructureLibrary.Services
{
    public class EventRepeatTypeService : IEventRepeatTypeService
    {
        private readonly IBaseRepository<EventRepeatType> _eventRepeatTypeRepository;
        private readonly IMapper _mapper;
        public EventRepeatTypeService(IBaseRepository<EventRepeatType> eventRepeatTypeRepository, IMapper mapper)
        {
            _eventRepeatTypeRepository = eventRepeatTypeRepository;
            _mapper = mapper;
        }
        public Task<int> CreateAsync(EventRepeatTypeDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventRepeatTypeDTO>> GetAllAsync()
        {
            var eventRepeatTypes = await _eventRepeatTypeRepository.GetAllAsync();

            return _mapper.Map<List<EventRepeatTypeDTO>>(eventRepeatTypes);
        }

        public Task<EventRepeatTypeDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, EventRepeatTypeDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
