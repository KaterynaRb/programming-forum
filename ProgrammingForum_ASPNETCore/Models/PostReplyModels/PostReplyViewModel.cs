using DAL.Entities;

namespace ProgrammingForum_ASPNETCore.Models.PostReplyModels
{
    public class PostReplyViewModel
    {
        public string ContentReply { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string AuthorName { get; set; }
        public byte[] AuthorPicture { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public PostReply? ParentReply { get; set; }
        public int? ParentReplyId { get; set; }
    }
}
