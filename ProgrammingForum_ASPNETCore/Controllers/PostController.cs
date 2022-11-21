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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPost _postService;

        public PostController(AppDbContext context, IMapper mapper, IPost postService)
        {
            _context = context;
            _mapper = mapper;
            _postService = postService;
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(PostCreateModel postModel)
        {
            if (postModel.Description == null || postModel.Content == null )
            {
                ViewBag.Message = "Description and content fields must be filled";
                return View();
            }

            postModel.AuthorName = User.Identity.Name;

            var post = _mapper.Map<Post>(postModel);

            _context.Posts.Add(post);
            _context.SaveChanges();

            int postId = post.Id;

            return RedirectToAction("ReadPost", new {id = postId});
        }

        public IActionResult ReadPost(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();

            var postView = _mapper.Map<PostViewModel>(post); //

            //if (_context.Users.Find(User.Identity.Name).Picture != null)
            //{
            //    postView.AuthorPicture = _context.Users.Find(User.Identity.Name).Picture;
            //}

            PostReplyCreateModel model = new PostReplyCreateModel();
            model.PostId = post.Id;

            postView.replyCreateModel = model;

            List<PostReplyViewModel> replyViews = new List<PostReplyViewModel>();
            List<PostReply> replies = _context.PostReplies.Where(pr => pr.PostId == post.Id).ToList();

            foreach(var r in replies)
            {
                var repView = _mapper.Map<PostReplyViewModel>(r);
                replyViews.Add(repView);
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

            //searchString = ViewData["CurrentFilter"].ToString();
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = _postService.GetPostsGlobalSearch(searchString, pageNumber, pageSize);
                allposts = _postService.GetPostsGlobalSearch(searchString);
                totalPages = (int)Math.Ceiling(allposts.Count() / (double)pageSize);
            }
            else
            {
                return View(); // Search is empty
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
