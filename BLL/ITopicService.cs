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
        Task<Topic> GetById(int id);
        Task<IEnumerable<Topic>> GetAll();
        Task<Topic> Add(Topic topic);
        Task Delete(int id);
        Task UpdateTopic(int id, Topic topic);
    }
}
