using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDislikeService
    {
        Dislike GetByPostAndUser(string userId, int postId);
        Dislike GetByPostReplyAndUser(string userId, int postReplyId);
        Task Add(Dislike dislike);
        Task DeleteOnPost(string userId, int postId);
        Task DeleteOnPostReply(string userId, int postReplyId);
    }
}
