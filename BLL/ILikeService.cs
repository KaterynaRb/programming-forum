using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ILikeService
    {
        Like GetByPostAndUser(string userId, int postId);
        Like GetByPostReplyAndUser(string userId, int postReplyId);


        Task Add(Like like);
        Task DeleteOnPost(string userId, int postId);
        Task DeleteOnPostReply(string userId, int postReplyId);
    }
}
