using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Dislike
    {
        public User User { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; } //pk
        public int PostId { get; set; } //pk
        public DateTime Date { get; set; }
        public PostReply PostReply { get; set; }
        public int? PostReplyId { get; set; } //may be null
    }
}
