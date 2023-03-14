using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkInfrastructureLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdDetailedAsync(id);

            return _mapper.Map<UserDTO>(user);
        }

        public Task<UserDTO> Login(User user)
        {
            throw new NotImplementedException();
        }
    }
}
