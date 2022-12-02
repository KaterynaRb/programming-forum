using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PostReplyService : IPostReplyService
    {
        private readonly AppDbContext _context;
        public PostReplyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddReplyToPost(PostReply postReply)
        {
            _context.PostReplies.Add(postReply);
            await _context.SaveChangesAsync();
        }
        public async Task AddReplyToReply(PostReply postReply, int parentId)
        {
            postReply.ParentReplyId = parentId;
            postReply.PostId = _context.PostReplies.Where(p => p.Id == parentId).FirstOrDefault().PostId;
            _context.PostReplies.Add(postReply);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostReply> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostReply GetById(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<PostReply> GetByPostId(int id)
        {
            return _context.PostReplies.Where(pr => pr.PostId == id).AsEnumerable();
        }

        public Task UpdateReplyContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }
    }
}
