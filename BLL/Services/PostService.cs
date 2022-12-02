using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.Include(post => post.PostReplies);
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(p => p.Id == id).FirstOrDefault();
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
                    return readTask.Result.Posts;
                }
                else
                {
                    return _context.Topics
                        .Where(topic => topic.Id == id)
                        .First()
                        .Posts
                        .OrderBy(x => x.Id); //and(!) date
                }
            }
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