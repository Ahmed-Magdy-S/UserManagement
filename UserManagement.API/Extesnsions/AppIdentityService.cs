using Microsoft.AspNetCore.Identity;
using UserManagement.Core.IdentityEntities;
using UserManagement.Core.Utils;
using UserManagement.Infrastructure.DatabaseContext;

namespace UserManagement.API.Extesnsions
{
    /// <summary>
    /// The class responsinle for adding identity service with configuration to app services
    /// </summary>
    public static class AppIdentityService
    {
        /// <summary>
        /// extension method for adding identity service with configuration to app services
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppIdentityService(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });
                
            //put it here as it considered to be use only with identity
            services.AddScoped<IPasswordHasher<AppUser>, Sha512PasswordHasher>();
        }
    }
}
