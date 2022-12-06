using DAL;
using DAL.Entities;
using ForumApiClient;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        private readonly Client _api;
        public PostService(AppDbContext context)
        {
            _context = context;
            _api = new Client("http://localhost:54962", new HttpClient());
        }

        public async Task Add(Post post)
        {
            await _api.PostPOSTAsync(post);
            //_context.Posts.Add(post);
            //await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _api.PostDELETEAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _api.PostAllAsync();
            //return _context.Posts.Include(post => post.PostReplies);
        }

        public async Task<Post> GetById(int id)
        {
            return await _api.PostGETAsync(id);
        }

        public int GetDislikesCount(int id)
        {
            return _context.Dislikes.Where(p => p.PostId == id).Count();
        }

        public int GetLikesCount(int id)
        {
            return _context.Likes.Where(p => p.PostId == id).Count();
        }

        public IEnumerable<Post> GetPostsByTopic(int id, int pageNumber, int pageSize)
        {
            return _context.Topics
                .Where(topic => topic.Id == id)
                .First()
                .Posts
                .OrderBy(x => x.Id) //and(!) date
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<Post> GetPostsByTopic(int id)
        {
            return _context.Topics
                        .Where(topic => topic.Id == id)
                        .First()
                        .Posts
                        .OrderBy(x => x.CreatedDate);
        }

        public IEnumerable<Post> GetPostsGlobalSearch(string searchString)
        {
            return _context.Posts
                .Where(p => p.Description.Contains(searchString) ||
                p.Content.Contains(searchString));
        }

        public IEnumerable<Post> GetPostsGlobalSearch(string searchString, int pageNumber, int pageSize)
        {
            return _context.Posts
                .Where(p => p.Description.Contains(searchString) ||
                p.Content.Contains(searchString))
                .OrderBy(x => x.Id) //and(!) date
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize); ;
        }

        public IEnumerable<Post> GetPostsInTopicSearch(int id, string searchString)
        {
            return _context.Posts.Where(
                p => p.TopicId == id &&
                (p.Description.Contains(searchString) ||
                p.Content.Contains(searchString)))
                .OrderBy(x => x.Id); //and(!) date
        }
        public IEnumerable<Post> GetPostsInTopicSearch(int id, string searchString, int pageNumber, int pageSize)
        {
            return _context.Posts.Where(
                p => p.TopicId == id &&
                (p.Description.Contains(searchString) ||
                p.Content.Contains(searchString)))
                .OrderBy(x => x.Id) //and(!) date
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public int GetRepliesCount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateDislikesCount(int postId, int value)
        {
            Post post = await _context.Posts.FindAsync(postId);
            post.DislikesCount += value;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLikesCount(int postId, int value)
        {
            Post post = await _context.Posts.FindAsync(postId);
            post.LikesCount += value;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(int id, Post post)
        {
            await _api.PostPUTAsync(id, post);
        }

        public Task UpdatePostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostDescription(int id, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}