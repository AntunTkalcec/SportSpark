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
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<Friendship, FriendshipDTO>();
            CreateMap<FriendshipDTO, Friendship>();
        }
    }
}
