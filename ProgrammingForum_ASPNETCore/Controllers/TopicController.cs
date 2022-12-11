using AutoMapper;
using BLL;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammingForum_ASPNETCore.Models.PostModels;
using ProgrammingForum_ASPNETCore.Models.TopicModels;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public TopicController(IMapper mapper, ITopicService topicService, IPostService postService)
        {
            _topicService = topicService;
            _postService = postService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(TopicCreateModel topicCreateModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        topicCreateModel.Image = fileBytes;
                    }
                }

                var findTopic = await _topicService.GetByName(topicCreateModel.Name);
                if (findTopic != null)
                {
                    ViewBag.TopicExists = "Topic with this title already exists";
                    return View(topicCreateModel);
                }

                Topic topic = _mapper.Map<Topic>(topicCreateModel);
                _topicService.Add(topic);

                return RedirectToAction("AllTopics");
            }
            return View(topicCreateModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllTopics()
        {
            IEnumerable<Topic> topics = await _topicService.GetAll();
            var topicViews = _mapper.Map<IEnumerable<TopicViewModel>>(topics);
            return View(topicViews);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllPostsInTopic(int id)
        {
            IEnumerable<Post> postsInTopic = _postService.GetPostsByTopic(id);
            var postViews = _mapper.Map<IEnumerable<PostViewModel>>(postsInTopic);
            Topic topic = await _topicService.GetById(id);
            ViewBag.Topic = topic.Name;
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

            Topic topic = await _topicService.GetById(id);
            ViewBag.Topic = topic.Name;
            return View(postListings);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _topicService.Delete(id);
            return RedirectToAction("AllTopics");
        }
    }
}
