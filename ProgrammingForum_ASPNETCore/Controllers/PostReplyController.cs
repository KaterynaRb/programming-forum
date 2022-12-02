using AutoMapper;
using BLL;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProgrammingForum_ASPNETCore.Models.PostModels;
using ProgrammingForum_ASPNETCore.Models.PostReplyModels;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class PostReplyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostReplyService _postReplyService;
        public PostReplyController(IMapper mapper, IPostReplyService postReplyService)
        {
            _mapper = mapper;
            _postReplyService = postReplyService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReplyToReply(int parentId, string contentReply)
        {
            PostReplyCreateModel replyCreateModel = new PostReplyCreateModel();
            replyCreateModel.AuthorName = User.Identity.Name;
            replyCreateModel.ContentReply = contentReply;

            var postReply = _mapper.Map<PostReply>(replyCreateModel);
            await _postReplyService.AddReplyToReply(postReply, parentId);

            List<PostReply> postReplies = _postReplyService.GetByPostId(postReply.PostId).ToList();
            List <PostReplyViewModel> repliesView = new List<PostReplyViewModel>();
            foreach (var reply in postReplies)
            {
                if (reply.ParentReplyId == null)
                {
                    var replyView = _mapper.Map<PostReplyViewModel>(reply);
                    repliesView.Add(replyView);
                }
            }
            return PartialView("_PostRepliesPartial", repliesView);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePostReply(PostReplyCreateModel replyCreateModel)
        {
            var postReply = _mapper.Map<PostReply>(replyCreateModel);
            await _postReplyService.AddReplyToPost(postReply);

            List<PostReply> postReplies = _postReplyService.GetByPostId(postReply.PostId).ToList();
            List<PostReplyViewModel> repliesView = new List<PostReplyViewModel>();
            foreach (var reply in postReplies)
            {
                if (reply.ParentReplyId == null)
                {
                    var replyView = _mapper.Map<PostReplyViewModel>(reply);
                    repliesView.Add(replyView);
                }
            }
            return PartialView("_PostRepliesPartial", repliesView);
        }
    }
}
