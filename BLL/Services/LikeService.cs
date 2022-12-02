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

        public async Task Delete(string userId, int itemId)
        {
            Like like = await _context.Likes.FindAsync(userId, itemId);
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Like> GetAll()
        {
            throw new NotImplementedException();
        }

        public Like GetById(string userId, int itemId)
        {
            return _context.Likes.Find(userId, itemId);
        }
    }
}
