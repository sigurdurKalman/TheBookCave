using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required]
        [EmailAddress(ErrorMessage="Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage="First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Password is required")]
        [MaxLength(ErrorMessage="The password must be at least 8 characters")]
        public string Password { get; set; }
    }
}