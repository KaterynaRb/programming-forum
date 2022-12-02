using DAL.Entities;

namespace ProgrammingForum_ASPNETCore.Models.UserModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public byte[]? Picture { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int Score { get; set; }
        public string? Role { get; set; }
        public IEnumerable<PostReply> PostReplies { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<Dislike> Dislikes { get; set; }
    }
}
