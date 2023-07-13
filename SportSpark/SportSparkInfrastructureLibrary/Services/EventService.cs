using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Enums;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Extensions;
using System.Linq;

namespace SportSparkInfrastructureLibrary.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IFriendshipRepository friendshipRepository, IBaseRepository<User> userRepository, IUserService userService,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(EventDTO entity)
        {
            if (!ValidateEvent(entity))
            {
                throw new Exception("Required fields cannot remain empty!");
            }
            await _eventRepository.AddAsync(_mapper.Map<Event>(entity));
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }

        public async Task<List<EventDTO>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync(e => e.User, e => e.EventType, e => e.RepeatType, e => e.User.RequestedFriendships, e => e.User.ReceivedFriendships);

            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<EventDTO> GetByIdAsync(int id)
        {
            var ev = await _eventRepository.GetByIdDetailedAsync(id);

            var test = _mapper.Map<EventDTO>(ev);

            return _mapper.Map<EventDTO>(ev);
        }

        public async Task UpdateAsync(int id, EventDTO entity)
        {
            if (!ValidateEvent(entity))
            {
                throw new Exception("Required fields cannot remain empty!");
            }
            await _eventRepository.UpdateAsync(_mapper.Map<Event>(entity));
        }

        public async Task<List<EventDTO>> GetInRadiusAsync(LatLongWrapperDTO wrapper, double radius, int userId)
        {
            var ids = await _eventRepository.GetEventsByLocation(wrapper, radius);

            var res = await _eventRepository.Fetch()
                .Include(x => x.User)
                .Include(x => x.RepeatType)
                .Include(x => x.EventType)
                .Where(x => ids.Contains(x.Id) && x.Active).ToListAsync();

            return _mapper.Map<List<EventDTO>>(res).Where(x => x.ValidUserIds.Contains(userId) || x.ValidUserIds.Count == 0).ToList();
        }

        public async Task<List<EventDTO>> GetUserEventsAsync(int userId, int loggedInUserId)
        {
            var res = await _eventRepository.Fetch()
                .Include(x => x.EventType)
                .Include(x => x.RepeatType)
                .Include(x => x.User)
                    .ThenInclude(_ => _.Events)
                .Include(x => x.User)
                    .ThenInclude(_ => _.ProfileImage)
                .Where(x => x.UserId == userId).ToListAsync();

            return _mapper.Map<List<EventDTO>>(res).Where(x => x.ValidUserIds.Contains(loggedInUserId) || x.ValidUserIds.Count == 0).ToList();
        }

        public async Task<List<EventDTO>> GetEventsByTermAsync(LatLongWrapperDTO wrapper, double radius, string term, int userId)
        {
            var ids = await _eventRepository.GetEventsByLocation(wrapper, radius);

            var res = await _eventRepository.Fetch()
                .Include(x => x.User)
                    .ThenInclude(x => x.RequestedFriendships)
                .Include(x => x.User)
                    .ThenInclude(x => x.ReceivedFriendships)
                .Include(x => x.RepeatType)
                .Include(x => x.EventType)
                .Where(x => (x.Title.Contains(term) || x.RepeatType.Description.Contains(term) || x.EventType.Name.Contains(term)) && ids.Contains(x.Id)
                && x.Active).ToListAsync();

            return _mapper.Map<List<EventDTO>>(res).Where(x => x.ValidUserIds.Contains(userId) || x.ValidUserIds.Count == 0).ToList();
        }

        public async Task<List<EventDTO>> GetUserFriendEventsAsync(int userId)
        {
            UserDTO user = _mapper.Map<UserDTO>(await _userRepository.Fetch()
                .Include(u => u.ReceivedFriendships)
                .Include(u => u.RequestedFriendships)
                .FirstOrDefaultAsync(u => u.Id == userId));

            List<FriendshipDTO> friendships = user.RequestedFriendships.Where(x => x.Status == (int)FriendshipStatus.Confirmed).ToList();
            friendships.AddRange(user.ReceivedFriendships.Where(x => x.Status == (int)FriendshipStatus.Confirmed));

            List<int> friendIds = new();
            foreach (FriendshipDTO friendship in friendships)
            {
                if (friendship.SenderId != userId)
                {
                    friendIds.Add(friendship.SenderId);
                }
                if (friendship.ReceiverId != userId)
                {
                    friendIds.Add(friendship.ReceiverId);
                }
            }

            List<Event> res = await _eventRepository.Fetch()
                .Include(x => x.EventType)
                .Include(x => x.RepeatType)
                .Where(x => x.Active && friendIds.Contains(x.UserId)).ToListAsync();

            return _mapper.Map<List<EventDTO>>(res).Where(x => x.ValidUserIds.Contains(userId) || x.ValidUserIds.Count == 0).ToList();
        }

        #region Private Methods
        private static bool ValidateEvent(EventDTO entity)
        {
            return !string.IsNullOrEmpty(entity.Title) &&
                !string.IsNullOrEmpty(entity.Description) &&
                (object)entity.Privacy is not null;
        }
        #endregion
    }
}
