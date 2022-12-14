using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingForum_ASPNETCore.Models.PostModels
{
    public class PostCreateModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 10)]
        [Display(Name = "Question")]
        public string Description { get; set; }

        [Required]
        [StringLength(3000, ErrorMessage = "Maximum length is {1}.")]
        [Display(Name = "Detailed description")]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
        //public string? Topic { get; set; }
        public int? TopicId { get; set; }
        public SelectList TopicOptions { get; set; }

        public PostCreateModel()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
