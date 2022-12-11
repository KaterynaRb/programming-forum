using System.ComponentModel.DataAnnotations;

namespace ProgrammingForum_ASPNETCore.Models.TopicModels
{
    public class TopicCreateModel
    {
        public string? Chapter { get; set; }
        public byte[]? Image { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
