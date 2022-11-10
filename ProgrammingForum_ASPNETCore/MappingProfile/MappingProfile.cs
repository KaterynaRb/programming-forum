using AutoMapper;
using DAL.Entities;
using ProgrammingForum_ASPNETCore.Models;

namespace ProgrammingForum_ASPNETCore.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCreateModel>().ReverseMap();

            CreateMap<Post, PostViewModel>()
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId)).ReverseMap();
        }
    }
}
