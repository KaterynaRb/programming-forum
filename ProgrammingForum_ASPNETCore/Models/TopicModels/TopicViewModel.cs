using DAL.Entities;

namespace ProgrammingForum_ASPNETCore.Models.TopicModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        public string? Chapter { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
