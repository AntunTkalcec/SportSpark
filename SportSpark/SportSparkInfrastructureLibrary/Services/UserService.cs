using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using System.Linq;

namespace SportSparkInfrastructureLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync(u => u.Events, u => u.ReceivedFriendships, u => u.RequestedFriendships, u => u.ProfileImage);

            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {            
            var user = await _userRepository.Fetch()
                .Include(u => u.Events)
                .Include(u => u.ReceivedFriendships)
                    .ThenInclude(_ => _.User1)
                .Include(u => u.RequestedFriendships)
                    .ThenInclude(_ => _.User2)
                .Include(u => u.ProfileImage)
                .FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserDTO>(user);
        }

        public Task<UserDTO> Login(User user)
        {
            throw new NotImplementedException();
        }
    }
}
