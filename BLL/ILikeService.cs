using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ILikeService
    {
        Like GetById(string userId, int itemId);
        IEnumerable<Like> GetAll();
        Task Add(Like like);
        Task Delete(string userId, int itemId);
    }
}
