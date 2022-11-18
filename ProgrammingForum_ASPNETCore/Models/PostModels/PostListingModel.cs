using Microsoft.EntityFrameworkCore;

namespace ProgrammingForum_ASPNETCore.Models.PostModels
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string AuthorName { get; set; }
        public int AuthorScore { get; set; }

        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicImage { get; set; }

        public int RepliesCount { get; set; }

    }
}
