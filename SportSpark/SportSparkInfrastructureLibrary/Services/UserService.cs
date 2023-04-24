﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SportSparkCoreSharedLibrary.Authentication.Models;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Authentication;
using SportSparkInfrastructureLibrary.Helpers;
using System.Security.Claims;

namespace SportSparkInfrastructureLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly TokenDataConfiguration _tokenDataConfiguration;

        public UserService(IBaseRepository<User> userRepository, IMapper mapper, ITokenService tokenService, 
            IOptions<TokenDataConfiguration> tokenDataConfiguration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _tokenDataConfiguration = tokenDataConfiguration.Value;
        }

        public async Task<int> CreateAsync(UserDTO entity)
        {
            if (!ValidateUser(entity))
            {
                throw new Exception("Required fields cannot remain empty!");
            }
            entity.Password = HashHelper.Hash(entity.Email, entity.Password);
            await _userRepository.AddAsync(_mapper.Map<User>(entity));
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
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

        public UserDTO Login(User user)
        {
            UserDTO userDto = _mapper.Map<UserDTO>(user);

            List<Claim> claims = new()
            {
                new Claim("UserId", user.Id.ToString())
            };

            AuthenticationInfo authInfo = new()
            {
                AccessToken = _tokenService.GenerateJwt(claims, _tokenDataConfiguration.AccessTokenExpirationInMinutes),
                RefreshToken = _tokenService.GenerateJwt(claims, _tokenDataConfiguration.RefreshTokenExpirationInMinutes)
            };
            userDto.AuthenticationInfo = authInfo;

            return userDto;
        }

        public async Task UpdateAsync(int id, UserDTO entity)
        {
            if (!ValidateUser(entity))
            {
                throw new Exception("Required fields cannot remain empty!");
            }
            await _userRepository.UpdateAsync(_mapper.Map<User>(entity));
        }

        public async Task<User> UserValid(string emailOrUserName, string password)
        {
            var currentEmailOrUserName = emailOrUserName.ToLower();
            var user = await _userRepository.Fetch().AsNoTracking().Where(user => user.Email == currentEmailOrUserName
                || user.UserName == currentEmailOrUserName).SingleOrDefaultAsync();

            if (user is not null && user.Password == HashHelper.Hash(user.Email, password)) return user;
            return null;
        }

        #region Private methods
        private static bool ValidateUser(UserDTO entity)
        {
            return !string.IsNullOrEmpty(entity.FirstName) &&
            !string.IsNullOrEmpty(entity.LastName) &&
            !string.IsNullOrEmpty(entity.UserName) &&
            !string.IsNullOrEmpty(entity.Email);
        }
        #endregion
    }
}
