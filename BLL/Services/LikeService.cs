using DAL;
using DAL.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LikeService : ILikeService
    {
        public readonly AppDbContext _context;
        public LikeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Like like)
        {
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOnPost(string userId, int postId)
        {
            Like like = _context.Likes.Where(l => l.UserId == userId && l.PostId == postId && l.PostReplyId == null).FirstOrDefault();
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOnPostReply(string userId, int postReplyId)
        {
            Like like = _context.Likes.Where(l => l.UserId == userId && l.PostReplyId == postReplyId).FirstOrDefault();
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public Like GetByPostAndUser(string userId, int postId)
        {
            Like like = _context.Likes.Where(l => l.UserId == userId && l.PostId == postId && l.PostReplyId == null).FirstOrDefault();
            return like;
        }

        public Like GetByPostReplyAndUser(string userId, int postReplyId)
        {
            Like like = _context.Likes.Where(l => l.UserId == userId && l.PostReplyId == postReplyId).FirstOrDefault();
            return like;
        }
    }
}
