using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreSharedLibrary.DTOs;
using System.Reflection.Metadata;
using System.Linq;
using Document = SportSparkCoreLibrary.Entities.Document;

namespace SportSparkAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Events, opt => opt.MapFrom(_ => _.Events)).MaxDepth(1);
            CreateMap<UserDTO, User>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<Friendship, FriendshipDTO>()
                .ForMember(x => x.Status, opt => opt.MapFrom(_ => (int)_.Status));
            CreateMap<FriendshipDTO, Friendship>();
            CreateMap<Event, EventDTO>()
                .ForMember(x => x.Privacy, opt => opt.MapFrom(_ => (int)_.Privacy))
                .ForMember(x => x.User, opt => opt.MapFrom(_ => _.User));
            CreateMap<EventDTO, Event>();
            CreateMap<EventRepeatType, EventRepeatTypeDTO>();
            CreateMap<EventRepeatTypeDTO, EventRepeatType>();
            CreateMap<EventType, EventTypeDTO>();
            CreateMap<EventTypeDTO, EventType>();
        }
    }
}
