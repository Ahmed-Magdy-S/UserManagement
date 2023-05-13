using UserManagement.Core.DTO;
using UserManagement.Core.IdentityEntities;

namespace UserManagement.Core.ServiceInterfaces
{
    public interface IJwtService
    {
       AuthenticationResponse CreateJwtToken(AppUser appUser);
    }
}
