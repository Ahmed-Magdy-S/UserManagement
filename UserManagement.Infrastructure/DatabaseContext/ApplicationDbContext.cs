using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.IdentityEntities;

namespace UserManagement.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    }
}
