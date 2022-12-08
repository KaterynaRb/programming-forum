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
    public class PostController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IPostReplyService _postReplyService;
        private readonly ILikeService _likeService;
        private readonly IDislikeService _dislikeService;

        public PostController(IMapper mapper, IPostService postService,
            IPostReplyService postReplyService, ILikeService likeService, IDislikeService dislikeService)
        {
            _mapper = mapper;
            _postService = postService;
            _postReplyService = postReplyService;
            _likeService = likeService;
            _dislikeService = dislikeService;
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateModel postModel)
        {
            if (postModel.Description == null || postModel.Content == null )
            {
                ViewBag.Message = "Description and content fields must be filled";
                return View();
            }
            postModel.AuthorName = User.Identity.Name;

            var post = _mapper.Map<Post>(postModel);
            await _postService.Add(post);

            int postId = post.Id;
            return RedirectToAction("ReadPost", new {id = postId});
        }

        public async Task<IActionResult> ReadPost(int id)
        {
            var post = await _postService.GetById(id);
            var postView = _mapper.Map<PostViewModel>(post);

            if (_likeService.GetByPostAndUser(User.Identity.Name, post.Id) != null) ViewData["liked"] = true;
            else ViewData["liked"] = false;

            if (_dislikeService.GetByPostAndUser(User.Identity.Name, post.Id) != null) ViewData["disliked"] = true;
            else ViewData["disliked"] = false;

            List<PostReplyViewModel> replyViews = new List<PostReplyViewModel>();
            List<PostReply> replies = _postReplyService.GetByPostId(post.Id).ToList();

            foreach (var r in replies)
            {
                if (r.ParentReplyId == null)
                {
                    var repView = _mapper.Map<PostReplyViewModel>(r);
                    replyViews.Add(repView);
                }
            }
            postView.PostReplies = replyViews;
            return View(postView);
        }

        public async Task<IActionResult> SearchPosts(string searchString, int? page)
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

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = _postService.GetPostsGlobalSearch(searchString, pageNumber, pageSize);
                allposts = _postService.GetPostsGlobalSearch(searchString);
                totalPages = (int)Math.Ceiling(allposts.Count() / (double)pageSize);
            }
            else
            {
                return View();
            }

            ViewData["prevDisabled"] = !(pageNumber > 1) ? "disabled" : "";
            ViewData["nextDisabled"] = !(pageNumber < totalPages) ? "disabled" : "";

            ViewData["pageIndex"] = pageNumber;
            ViewData["CurrentFilter"] = searchString;

            var postListings = _mapper.Map<IEnumerable<PostListingModel>>(posts);

            return View(postListings);
        }
    }
}
