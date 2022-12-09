using DAL;
using DAL.Entities;
using ForumApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TopicService : ITopicService
    {
        private readonly AppDbContext _context;
        private readonly Client _api;

        public TopicService(AppDbContext context)
        {
            _context = context;
            _api = new Client("http://localhost:54962", new HttpClient());
        }

        public async Task<Topic> Add(Topic topic)
        {
            return await _api.TopicPOSTAsync(topic);
        }

        public async Task Delete(int id)
        {
            await _api.TopicDELETEAsync(id);
        }

        public async Task<IEnumerable<Topic>> GetAll()
        {
            return await _api.TopicAllAsync();
        }

        public async Task<Topic> GetById(int id)
        {
            return await _api.TopicGETAsync(id);
        }

        public async Task UpdateTopic(int id, Topic topic)
        {
            await _api.TopicPUTAsync(id, topic);
        }
    }
}
