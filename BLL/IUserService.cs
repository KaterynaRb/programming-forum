using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IUserService
    {
        Task<User> GetById(string id);
        Task<IEnumerable<User>> GetAll();

        Task<User> GetByIdAndEmail(string id, string email);

        Task<User> Add(User user);
        Task Delete(string id);
        Task Update(string id, User user);
    }
}
