using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TopicService : ITopic
    {
        private readonly AppDbContext _context;
        public TopicService(AppDbContext context)
        {
            _context = context;
        }

        public Task Create(Topic post)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAll()
        {
            return _context.Topics.Include(topic => topic.Posts);
        }

        public Topic GetById(int id)
        {
            return _context.Topics.Find(id);
        }

        public Task UpdateTopicName(int id, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
