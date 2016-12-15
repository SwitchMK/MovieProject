using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=(?:.*?[!@#$%\^&*\(\)\-_+=;:'""\/\[\]{},.<>|`]){1}).{6,}$",
            ErrorMessage = "The password must contain at least 6 characters, at least one in upper case, at least one number and at least one special character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
