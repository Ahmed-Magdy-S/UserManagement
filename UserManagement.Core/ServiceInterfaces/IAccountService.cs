using Microsoft.AspNetCore.Identity;
using UserManagement.Core.DTO;
using UserManagement.Core.IdentityEntities;

namespace UserManagement.Core.ServiceInterfaces
{
    public interface IAccountService
    {
        AppUser CreateIdentityUser(RegisterDto registerDto);
        Task<IdentityResult> InsertIntoDbAsync(AppUser user, string password);
        Task<bool> CheckUserPassword(AppUser user, string password);
        Task<bool> IsUserNameExist(string username);
        Task<AppUser?> FindUserByUserNameAsync(string userName);
        Task<AppUser?> FindUserByIdAsync(string userId);
        Task<ProfileDto?> GetUserProfile(string userId, string password);
        Task<IdentityResult> ResetUserPassword(AppUser appUser, ResetPasswordDto resetPasswordDto);
        Task<IdentityResult> UpdateUserProfile(AppUser appUser, ProfileUpdateDto profileUpdateDto);
    }
}
