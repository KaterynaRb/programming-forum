using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public virtual User Author { get; set; }
        public string UserId { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
        public virtual PostReply? ParentReply { get; set; }
        public int? ParentReplyId { get; set; }
        public virtual IEnumerable<PostReply>? Replies { get; set; }
        public virtual IEnumerable<Like> Likes { get; set; }
        public virtual IEnumerable<Dislike> Dislikes { get; set; }
    }
}
