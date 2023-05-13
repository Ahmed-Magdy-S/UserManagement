using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Please provide your UserName")]
        public required string UserName { get; set; }


        [Required(ErrorMessage = "Please provide your FirstName")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide your FatherName")]
        public required string FatherName { get; set; }

        [Required(ErrorMessage = "Please provide your FamilyName")]
        public required string FamilyName { get; set; }

        [Required(ErrorMessage = "Please provide your address")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "Please provide your birthdate")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Please provide your password")]
        public required string Password { get; set; }
    }
}
