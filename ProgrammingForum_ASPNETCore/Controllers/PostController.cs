using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingForum_ASPNETCore.Models;
using System.Security.Principal;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PostController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(PostViewModel postModel)
        {
            if (postModel.Description == null || postModel.Content == null )
            {
                ViewBag.Message = "Description and content fields must be filled";
                return View();
            }

            postModel.CreatedDate = DateTime.Now;
            postModel.AuthorName = User.Identity.Name;

            //add post to database
            var postmap = _mapper.Map<Post>(postModel);

            //_context.Posts.Add(postmap);
            //_context.SaveChanges();

            return View("Post", postModel);
        }
    }
}
