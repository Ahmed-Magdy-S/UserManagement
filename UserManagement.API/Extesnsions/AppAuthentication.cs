using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserManagement.API.Extesnsions
{
    /// <summary>
    /// Providing extension method to manage app authentication and configure it
    /// </summary>
    public static class AppAuthentication
    {
        /// <summary>
        /// Add configured authenticaiton to the app 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddConfiguredAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = config["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = config["Jwt:Issure"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "wrong")),
                };
            });
        }
    }
}
