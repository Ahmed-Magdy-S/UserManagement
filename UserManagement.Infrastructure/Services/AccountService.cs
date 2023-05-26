using Microsoft.AspNetCore.Identity;
using UserManagement.Core.DTO;
using UserManagement.Core.IdentityEntities;
using UserManagement.Core.ServiceInterfaces;
using UserManagement.Core.Utils;

namespace UserManagement.Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckUserPassword(AppUser user,string password)
        {
           return await _userManager.CheckPasswordAsync(user,password);
        }

        public AppUser CreateIdentityUser(RegisterDto registerDto)
        {
            AppUser user = new()
            {
                FirstName = registerDto.FirstName,
                FatherName = registerDto.FatherName,
                FamilyName = registerDto.FamilyName,
                UserName = registerDto.UserName,
                Address = AesEncryption.Encrypt(registerDto.Address,registerDto.Password),
                Birthdate = AesEncryption.Encrypt(registerDto.Birthdate.ToString(),registerDto.Password)
            };

            return user;
        }

        public async Task<AppUser?> FindUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<AppUser?> FindUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ProfileDto?> GetUserProfile(string userId, string password)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null) return null;

            ProfileDto profileDto = new()
            {
                FirstName = appUser.FirstName,
                FatherName = appUser.FatherName,
                FamilyName = appUser.FamilyName,
                UserName = appUser.UserName,
                Address = AesEncryption.Decrypt(appUser.Address, password),
                Birthdate = AesEncryption.Decrypt(appUser.Birthdate, password)
            };

            return profileDto;

        }

        public async Task<IdentityResult> InsertIntoDbAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> IsUserNameExist(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

        public Task<IdentityResult> ResetUserPassword(AppUser appUser, ResetPasswordDto resetPasswordDto)
        {
            return _userManager.ChangePasswordAsync(appUser,resetPasswordDto.OldPassword,resetPasswordDto.NewPassword);
        }

        public async Task<IdentityResult> UpdateUserAccount(AppUser appUser,string password,ProfileDto profileDto)
        {
            appUser.FirstName = profileDto.FirstName;
            appUser.FatherName = profileDto.FatherName;
            appUser.FamilyName = profileDto.FamilyName;
            appUser.UserName = profileDto.UserName;
            appUser.Address = AesEncryption.Encrypt(profileDto.Address, password);
            appUser.Birthdate = AesEncryption.Encrypt(profileDto.Birthdate, password);

            return await _userManager.UpdateAsync(appUser);

        }
    }
}
