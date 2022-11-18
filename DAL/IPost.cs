using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByTopic(int id, int pageNumber, int pageSize);
        IEnumerable<Post> GetPostsByTopic(int id);
        IEnumerable<Post> GetPostsGlobalSearch(string searchString);
        IEnumerable<Post> GetPostsGlobalSearch(string searchString, int pageNumber, int pageSize);

        IEnumerable<Post> GetPostsInTopicSearch(int id, string searchString);
        IEnumerable<Post> GetPostsInTopicSearch(int id, string searchString, int pageNumber, int pageSize);
        Task Create(Post post);
        Task Delete(int id);
        Task UpdatePostDescription(int id, string newDescription);
        Task UpdatePostContent(int id, string newContent);
    }
}
