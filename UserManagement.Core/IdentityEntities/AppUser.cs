using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core.IdentityEntities
{
    public class AppUser : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }

        public required string FatherName { get; set; }

        public required string FamilyName { get; set; }

        public required byte[] Address { get; set; }

        public required byte[] Birthdate { get; set; }

    }
}
