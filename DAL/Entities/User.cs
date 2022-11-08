using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Picture { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int Score { get; set; }
        public string Role { get; set; }
        public virtual IEnumerable<PostReply> PostReplies { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
        public virtual IEnumerable<Like> Likes { get; set; }
        public virtual IEnumerable<Dislike> Dislikes { get; set; }
    }
}
