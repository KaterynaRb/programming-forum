using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DislikeService : IDislikeService
    {
        private readonly AppDbContext _context;
        public DislikeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Dislike dislike)
        {
            await _context.Dislikes.AddAsync(dislike);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOnPost(string userId, int postId)
        {
            Dislike dislike = _context.Dislikes.Where(l => l.UserId == userId && l.PostId == postId).FirstOrDefault();
            _context.Dislikes.Remove(dislike);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOnPostReply(string userId, int postReplyId)
        {
            Dislike dislike = _context.Dislikes.Where(l => l.UserId == userId && l.PostReplyId == postReplyId).FirstOrDefault();
            _context.Dislikes.Remove(dislike);
            await _context.SaveChangesAsync();
        }

        public Dislike GetByPostAndUser(string userId, int postId)
        {
            Dislike dislike = _context.Dislikes.Where(l => l.UserId == userId && l.PostId == postId).FirstOrDefault();
            return dislike;
        }
        public Dislike GetByPostReplyAndUser(string userId, int postReplyId)
        {
            Dislike dislike = _context.Dislikes.Where(l => l.UserId == userId && l.PostReplyId == postReplyId).FirstOrDefault();
            return dislike;
        }
    }
}
