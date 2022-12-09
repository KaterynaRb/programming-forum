using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        public string? Chapter { get; set; }
        public virtual IEnumerable<Post>? Posts { get; set; }
    }
}
