using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkInfrastructureLibrary.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IMapper _mapper;
        public FriendshipService(IFriendshipRepository friendshipRepository, IMapper mapper)
        {
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
        }
        public Task<int> CreateAsync(FriendshipDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FriendshipDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FriendshipDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FriendshipDTO>> GetReceivedFriendshipsAsync(int id)
        {
            var res = await _friendshipRepository.GetReceivedFriendshipsAsync(id);

            return _mapper.Map<List<FriendshipDTO>>(res);
        }

        public async Task UpdateAsync(int id, FriendshipDTO entity)
        {
            await _friendshipRepository.UpdateAsync(_mapper.Map<Friendship>(entity));
        }
    }
}
