using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreSharedLibrary.DTOs;
using System.Reflection.Metadata;
using Document = SportSparkCoreLibrary.Entities.Document;

namespace SportSparkAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Friendships, opt => opt.MapFrom(_ => _.RequestedFriendships));
            CreateMap<UserDTO, User>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<Friendship, FriendshipDTO>()
                .ForMember(x => x.Status, opt => opt.MapFrom(_ => (int)_.Status));
            CreateMap<FriendshipDTO, Friendship>();
        }
    }
}
