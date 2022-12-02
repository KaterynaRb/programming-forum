using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IPostReplyService
    {
        PostReply GetById(int id);
        IEnumerable<PostReply> GetAll();
        IEnumerable<PostReply> GetByPostId(int id);
        Task AddReplyToPost(PostReply postReply);
        Task AddReplyToReply(PostReply postReply, int parentId);

        Task Delete(int id);
        Task UpdateReplyContent(int id, string newContent);
    }
}
