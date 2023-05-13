using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UserManagement.API.Extesnsions
{
    /// <summary>
    /// Provide swagger service for app documentation
    /// </summary>
    public static class SwaggerService
    {
        /// <summary>
        /// Add configured swagger service
        /// </summary>
        /// <param name="services"></param>
        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen((options) =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                      new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new String[]{ }
                    }
                }
                );

            });
        }
    }
}
