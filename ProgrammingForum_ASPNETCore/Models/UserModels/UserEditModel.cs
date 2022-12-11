using System.ComponentModel.DataAnnotations;

namespace ProgrammingForum_ASPNETCore.Models.UserModels
{
    public class UserEditModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Picture")]
        public byte[]? Picture { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
    }
}
