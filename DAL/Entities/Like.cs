using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime Date { get; set; }
        public virtual PostReply? PostReply { get; set; }
        public int? PostReplyId { get; set; }

        //[Required]
        //public string NotNullProperties { get { return PostId.HasValue && PostReplyId.HasValue; } private set { } }
    }
}
