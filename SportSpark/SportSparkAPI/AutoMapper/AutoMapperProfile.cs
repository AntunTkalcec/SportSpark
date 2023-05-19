using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreSharedLibrary.DTOs;
using Document = SportSparkCoreLibrary.Entities.Document;

namespace SportSparkAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.RequestedFriendships, opt => opt.MapFrom(_ => _.RequestedFriendships)).MaxDepth(2)
                .ForMember(x => x.ReceivedFriendships, opt => opt.MapFrom(_ => _.ReceivedFriendships)).MaxDepth(2)
                .ForMember(x => x.Events, opt => opt.MapFrom(_ => _.Events)).MaxDepth(2)
                .ForMember(x => x.Password, opt => opt.Ignore());
            CreateMap<UserDTO, User>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<Friendship, FriendshipDTO>()
                .ForMember(x => x.Status, opt => opt.MapFrom(_ => (int)_.Status));
            CreateMap<FriendshipDTO, Friendship>()
                .ForMember(x => x.Sender, opt => opt.Ignore())
                .ForMember(x => x.Receiver, opt => opt.Ignore());
            CreateMap<Event, EventDTO>()
                .ForMember(x => x.Privacy, opt => opt.MapFrom(_ => (int)_.Privacy))
                .ForMember(x => x.User, opt => opt.MapFrom(_ => _.User));
            CreateMap<EventDTO, Event>()
                .ForMember(x => x.EventTypeId, opt => opt.MapFrom(_ => _.EventType.Id))
                .ForMember(x => x.RepeatTypeId, opt => opt.MapFrom(_ => _.RepeatType.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(_ => _.User.Id))
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.EventType, opt => opt.Ignore())
                .ForMember(x => x.RepeatType, opt => opt.Ignore());
            CreateMap<EventRepeatType, EventRepeatTypeDTO>();
            CreateMap<EventRepeatTypeDTO, EventRepeatType>();
            CreateMap<EventType, EventTypeDTO>();
            CreateMap<EventTypeDTO, EventType>();
        }
    }
}
