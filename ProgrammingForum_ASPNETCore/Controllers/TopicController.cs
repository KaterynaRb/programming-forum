using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingForum_ASPNETCore.Models;
using ProgrammingForum_ASPNETCore.Models.PostModels;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopic _topicService;
        private readonly IPost _postService;
        private readonly IMapper _mapper;

        public TopicController(IMapper mapper, ITopic topicService, IPost postService)
        {
            _topicService = topicService;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Posts(int id, string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumber = 1;
            if (page.HasValue)
            {
                pageNumber = (int)page;
            }
            IEnumerable<Post> posts;
            IEnumerable<Post> allposts;
            int totalPages;

            //searchString = ViewData["CurrentFilter"].ToString();
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = _postService.GetPostsInTopicSearch(id, searchString, pageNumber, pageSize);
                allposts = _postService.GetPostsInTopicSearch(id, searchString);
                totalPages = (int)Math.Ceiling(allposts.Count() / (double)pageSize);
            }
            else
            {
                posts = _postService.GetPostsByTopic(id, pageNumber, pageSize);
                allposts = _postService.GetPostsByTopic(id);
                totalPages = (int)Math.Ceiling(allposts.Count() / (double)pageSize);
            }


            ViewData["prevDisabled"] = !(pageNumber > 1) ? "disabled" : "";
            ViewData["nextDisabled"] = !(pageNumber < totalPages) ? "disabled" : "";

            ViewData["pageIndex"] = pageNumber;
            ViewData["CurrentFilter"] = searchString;

            var postListings = _mapper.Map<IEnumerable<PostListingModel>>(posts);

            return View(postListings);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
