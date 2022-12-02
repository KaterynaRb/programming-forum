using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54962/api/");
                //HTTP GET
                var responseTask = client.GetAsync("User");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<User>>();
                    readTask.Wait();
                    return readTask.Result;
                }
                else
                {
                    return _context.Users;
                }
            }
        }

        public User GetById(string id)
        {
            return _context.Users.Where(u => u.UserName == id).FirstOrDefault();
        }

        public User GetByIdAndEmail(string id, string email)
        {
            return _context.Users
                    .Where(u => u.UserName == id
                    || u.Email == email).FirstOrDefault();
        }

        public Task Update(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
