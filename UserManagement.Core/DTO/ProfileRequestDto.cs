using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core.DTO
{
    public class ProfileRequestDto
    {
        [Required(ErrorMessage = "You need to enter your password")]
        public required string Password { get; set; }
    }
}
