using DAL.Entities;
using ProgrammingForum_ASPNETCore.Models.PostModels;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingForum_ASPNETCore.Models.PostReplyModels
{
    public class PostReplyCreateModel
    {
        [StringLength(3000, ErrorMessage = "Maximum length is {1}.")]
        public string ContentReply { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
        public int PostId { get; set; }
        public PostReply? ParentReply { get; set; }
        public int? ParentReplyId { get; set; }
        
        public PostReplyCreateModel()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
