using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.DTO
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Enter the current passowrd")]
        public required string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter the new passowrd")]
        public required string NewPassword { get; set; }

    }
}
