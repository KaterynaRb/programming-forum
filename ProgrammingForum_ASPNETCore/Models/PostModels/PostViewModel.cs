using DAL.Entities;
using ProgrammingForum_ASPNETCore.Models.PostReplyModels;

namespace ProgrammingForum_ASPNETCore.Models.PostModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public PostReply? AcceptedReply { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string AuthorName { get; set; }
        public byte[] AuthorPicture { get; set; }
        public Topic? Topic { get; set; }

        //public PostReplyCreateModel replyCreateModel { get; set; }
        public IEnumerable<PostReplyViewModel> PostReplies { get; set; }
    }
}
