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

        public async Task Delete(string userId, int itemId)
        {
            Dislike dislike = await _context.Dislikes.FindAsync(userId, itemId);
            _context.Dislikes.Remove(dislike);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Dislike> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dislike GetById(string userId, int itemId)
        {
            return _context.Dislikes.Find(userId, itemId);
        }
    }
}
