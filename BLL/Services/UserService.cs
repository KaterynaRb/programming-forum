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
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly Client _api;

        public UserService(AppDbContext context)
        {
            _context = context;
            _api = new Client("http://localhost:54962", new HttpClient());
        }
        public async Task Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _api.UserPOSTAsync(user);
        }

        public async Task Delete(string id)
        {
            await _api.UserDELETEAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _api.UserAllAsync();
        }

        public async Task<User> GetById(string id)
        {
            return await _api.UserGETAsync(id);
        }

        public async Task<User> GetByIdAndEmail(string id, string email)
        {
            return await _context.Users
                    .Where(u => u.UserName == id
                    || u.Email == email).FirstOrDefaultAsync();
        }

        public async Task Update(string id, User user)
        {
            await _api.UserPUTAsync(id, user);
        }
    }
}
