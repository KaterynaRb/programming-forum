using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingForum_ASPNETCore.Models.UserModels
{
    public class UserCreateModel
    {
        public byte[]? Picture { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be more then {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [Compare(nameof(Email), ErrorMessage = "Emails don't match.")]
        [Display(Name = "Confirm email")]
        public string ConfirmEmail { get; set; }

        public DateTime RegisteredDate { get; set; }

        public UserCreateModel()
        {
            RegisteredDate = DateTime.Now;
        }
    }
}
