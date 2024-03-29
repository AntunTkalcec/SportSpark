﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

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
            var ev = await _eventRepository.GetByIdAsync(id, e => e.User, e => e.EventType, e => e.RepeatType);

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
                    .ThenInclude(x => x.RequestedFriendships)
                .Include(x => x.User)
                    .ThenInclude(x => x.ReceivedFriendships)
                .Include(x => x.User)
                    .ThenInclude(x => x.ProfileImage)
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
