using AutoMapper;
using DAL.Entities;
using ProgrammingForum_ASPNETCore.Models.PostModels;
using ProgrammingForum_ASPNETCore.Models.PostReplyModels;
using ProgrammingForum_ASPNETCore.Models.TopicModels;
using ProgrammingForum_ASPNETCore.Models.UserModels;

namespace ProgrammingForum_ASPNETCore.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCreateModel>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<User, UserEditModel>().ReverseMap();

            CreateMap<Topic, TopicViewModel>().ReverseMap();

            CreateMap<Topic, TopicCreateModel>().ReverseMap();

            CreateMap<Post, PostListingModel>()
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId))
                .ForMember(
                dest => dest.AuthorScore,
                opt => opt.MapFrom(src => src.Author.Score))
                //.ForMember(
                //dest => dest.TopicName,
                //opt => opt.MapFrom(src => src.Topic.Name))
                .ReverseMap();

            CreateMap<Post, PostViewModel>()
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId)).ReverseMap();

            CreateMap<Post, PostCreateModel>()
                .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

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
        }
    }
}
