﻿using AutoMapper;
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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PostReplyController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //[Authorize]
        [HttpPost]
        public IActionResult CreateReplyToReply(int parentId, string contentReply)
        {
            PostReplyCreateModel replyCreateModel = new PostReplyCreateModel();
            replyCreateModel.AuthorName = User.Identity.Name; //redirect to login
            replyCreateModel.ParentReplyId = parentId;
            replyCreateModel.ContentReply = contentReply;
            replyCreateModel.PostId = _context.PostReplies.Where(p => p.Id == parentId).FirstOrDefault().PostId;

            var postReply = _mapper.Map<PostReply>(replyCreateModel);

            _context.PostReplies.Add(postReply);
            _context.SaveChanges();


            List<PostReply> postReplies = _context.PostReplies.Where(pr => pr.PostId == postReply.PostId).ToList();
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


        //[Authorize]
        [HttpPost]
        public IActionResult CreatePostReply(PostReplyCreateModel replyCreateModel)
        {
            //replyCreateModel.AuthorName = User.Identity.Name; //redirect to login

            var postReply = _mapper.Map<PostReply>(replyCreateModel);

            _context.PostReplies.Add(postReply);
            _context.SaveChanges();


            List<PostReply> postReplies = _context.PostReplies.Where(pr => pr.PostId == postReply.PostId).ToList();
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
