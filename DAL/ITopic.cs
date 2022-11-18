using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ITopic
    {
        Topic GetById(int id);
        IEnumerable<Topic> GetAll();

        Task Create(Topic post);
        Task Delete(int id);
        Task UpdateTopicName(int id, string newName);
    }
}
