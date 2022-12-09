using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IPostService
    {
        Task<Post> GetById(int id);
        Task<IEnumerable<Post>> GetAll();

        IEnumerable<Post> GetPostsByTopic(int id);
        IEnumerable<Post> GetPostsByTopic(int id, int pageNumber, int pageSize);

        IEnumerable<Post> GetPostsGlobalSearch(string searchString);
        IEnumerable<Post> GetPostsGlobalSearch(string searchString, int pageNumber, int pageSize);

        IEnumerable<Post> GetPostsInTopicSearch(int id, string searchString);
        IEnumerable<Post> GetPostsInTopicSearch(int id, string searchString, int pageNumber, int pageSize);

        Task<Post> Add(Post post);
        Task UpdateLikesCount(int postId, int value);
        Task UpdateDislikesCount(int postId, int value);

        int GetLikesCount(int id);
        int GetDislikesCount(int id);
        int GetRepliesCount(int id);

        Task Delete(int id);
        Task UpdatePost(int id, Post post);
        Task UpdatePostDescription(int id, string newDescription);
        Task UpdatePostContent(int id, string newContent);
    }
}
