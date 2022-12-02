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
        User GetById(string id);
        IEnumerable<User> GetAll();

        User GetByIdAndEmail(string id, string email);

        Task Add(User user);
        Task Delete(string id);
        Task Update(string id, User user);
    }
}
