using DAL.Entities;

namespace ProgrammingForum_ASPNETCore.Models
{
    public class PostViewModel
    {
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public PostReply? AcceptedReply { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string AuthorName { get; set; }
        public string? Topic { get; set; }
        public IEnumerable<PostReply> PostReplies { get; set; }
    }
}
