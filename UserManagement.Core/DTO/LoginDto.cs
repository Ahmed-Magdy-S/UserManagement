using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "You must enter your username")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You must enter your password")]
        public required string Password { get; set; }
    }
}
