using DAL;
using DAL.Entities;
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
        public TopicService(AppDbContext context)
        {
            _context = context;
        }

        public Task Add(Topic post)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54962/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Topic");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<Topic>>();
                    readTask.Wait();
                    return readTask.Result;
                }
                else
                {
                    return _context.Topics;
                }
            }
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
