using Core;
using data;
using data.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service;
using System.Text;

namespace WebApi {
    public static class ServiceCollectionExtensions {
        public static void AddAppServices(this IServiceCollection services) {
            services.AddScoped<BlogPostManager>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IUserRepositroy, UserRepository>();
            services.AddScoped<UserService>();
        }

        public static void AddStaticFilesManager(this IServiceCollection services, string webRootPath) {
            services.AddSingleton(new StaticFilesManager(webRootPath));
        }

        public static void AddPostgreSQL(this IServiceCollection services) {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseLazyLoadingProxies()
                   .UseNpgsql(AppSettings.Database.ConnectionString)
            );
        }

        public static void AddAppIdentity(this IServiceCollection services) {
            services.AddIdentity<User, IdentityRole>(opt => {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;

                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();
        }

        public static void AddJwtAuthentication(this IServiceCollection services) {
            // Set the default scheme for entire authentication process to JWT instead of Cookies
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AppSettings.JwtToken.Issuer,
                    ValidAudience = AppSettings.JwtToken.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtToken.SecurityKey))
                };
            });
        }

        public static void AddAppCors(this IServiceCollection services) {
            services.AddCors(opt => {
                opt.AddPolicy(AppSettings.Cors.Name, policy => {
                    policy.WithOrigins(AppSettings.Cors.TrustedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }
    }
}
