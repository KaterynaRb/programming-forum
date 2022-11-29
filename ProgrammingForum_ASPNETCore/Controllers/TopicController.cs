using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingForum_ASPNETCore.Models;
using ProgrammingForum_ASPNETCore.Models.PostModels;
using ProgrammingForum_ASPNETCore.Models.TopicModels;

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


        [Authorize(Roles = "Admin")]
        public IActionResult AllTopics()
        {
            IEnumerable<Topic> topics = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54962/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Topic");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<Topic>>();
                    readTask.Wait();
                    topics = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            var topicViews = _mapper.Map<IEnumerable<TopicViewModel>>(topics);
            return View(topicViews);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllPostsInTopic(int id)
        {
            IEnumerable<Post> postsInTopic = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54962/api/Topic/");
                //HTTP GET
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Topic>();
                    readTask.Wait();
                    postsInTopic = readTask.Result.Posts;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            var postViews = _mapper.Map<IEnumerable<PostViewModel>>(postsInTopic);
            return View(postViews);
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
