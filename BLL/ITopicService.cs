using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ITopicService
    {
        Topic GetById(int id);
        IEnumerable<Topic> GetAll();

        Task Add(Topic post);
        Task Delete(int id);
        Task UpdateTopicName(int id, string newName);
    }
}
