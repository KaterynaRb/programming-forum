using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? AcceptedReplyId { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public virtual User Author { get; set; }
        public string UserId { get; set; }
        public virtual Topic? Topic { get; set; }
        public int? TopicId { get; set; }
        public virtual IEnumerable<PostReply>? PostReplies { get; set; }
        public virtual IEnumerable<Like> Likes { get; set; }
        public virtual IEnumerable<Dislike> Dislikes { get; set; }
    }
}
