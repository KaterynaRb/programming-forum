using System.ComponentModel.DataAnnotations;

namespace ProgrammingForum_ASPNETCore.Models
{
    public class UserCreateModel
    {
        [Required]
        public string UserName { get; set; }
        public byte[] Picture { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ConfirmEmail { get; set; }
        public DateTime RegisteredDate { get; set; }
        public UserCreateModel()
        {
            this.RegisteredDate = DateTime.Now;
        }
    }
}
