using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDislikeService
    {
        Dislike GetById(string userId, int itemId);
        IEnumerable<Dislike> GetAll();
        Task Add(Dislike dislike);
        Task Delete(string userId, int itemId);
    }
}
