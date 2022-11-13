using AutoMapper;
using DAL.Entities;
using ProgrammingForum_ASPNETCore.Models.PostModels;
using ProgrammingForum_ASPNETCore.Models.PostReplyModels;
using ProgrammingForum_ASPNETCore.Models.UserModels;

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

            CreateMap<Post, PostCreateModel>()
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId)).ReverseMap();

            CreateMap<PostViewModel, PostCreateModel>().ReverseMap();


            CreateMap<PostReply, PostReplyCreateModel>()
                .ForMember(
                dest => dest.ContentReply,
                opt => opt.MapFrom(src => src.Content))
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<PostReply, PostReplyViewModel>()
                .ForMember(
                dest => dest.ContentReply,
                opt => opt.MapFrom(src => src.Content))
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            //CreateMap<PostReplyViewModel, PostReplyCreateModel>().ReverseMap();
        }
    }
}
