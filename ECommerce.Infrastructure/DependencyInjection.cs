using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.QueryFilters;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Infrastructure.Authentication;
using ECommerce.Infrastructure.BackgroundJobs.OrderBackgroundService;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.QueryFilters;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services.EmailService;
using ECommerce.Infrastructure.Services.HangfireBackgroundJobService;
using ECommerce.Infrastructure.Services.StripeService;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("MyDB");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add JWT config
            // 1. Bind JwtOptions (bind dữ liệu cấu hình từ appsettings.json vào class JwtOptions thông qua DI
            // nhằm sử dụng để có thể inject IOptions<JwtOptions> cho JwtProvider để tạo token)
            // tên trong appsettings.json phải khớp với tên property trong class ( không phân biệt hoa thường).
            services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));

            // 2. Đăng ký JwtProvider (service tạo token)
            services.AddScoped<IJwtProvider, JwtProvider>();

            // 3. Cấu hình Authentication JWT Bearer
            // Lấy giá trị config JwtOptions một cách thủ công(không thông qua DI) – vì ngay sau đó ta dùng jwtOptions
            // để thiết lập thông tin cho JWT Bearer.
            // GetSection("JwtSettings") => trỏ tới phần "JwtSettings" trong appsettings.json.
            // .Get<JwtOptions>() => ánh xạ sang class JwtOptions.

            // lấy cấu hình Jwt từ appsetting sang class JwtOptions
            JwtOptions? jwtOptions = configuration.GetSection("JwtSettings").Get<JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //cấu hình cách JWT được xác minh và kiểm tra.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IDateFilterService, DateFilterServices>();
            services.AddScoped<IBackgroundService, HangfireBackgroundJobService>();
            services.AddScoped<IOrderBackgroundService, OrderBackgroundService>();
            services.Configure<EmailSettings>(configuration.GetSection("SmtpSettings"));

            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            // config Hangfire
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(connectionString);
            });
            services.AddHangfireServer();

            return services;
        }
    }
}
