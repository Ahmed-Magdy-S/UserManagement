using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.DTO
{
    public class ProfileUpdateDto : ProfileDto
    {
        [Required(ErrorMessage = "You must provide your password")]
        public required string Password { get; set; }
    }
}
