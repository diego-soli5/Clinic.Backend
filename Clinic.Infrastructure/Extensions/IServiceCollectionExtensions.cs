using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Options;
using Clinic.Core.Services;
using Clinic.Infrastructure.Data;
using Clinic.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Clinic.Core.Interfaces.EmailServices;
using Clinic.Infrastructure.EmailService;
using Clinic.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Clinic.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DevelopmentLocalDb"));
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddBusisnessServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IAccountService, AccountService>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

                return new UriService(absoluteUri);
            });

            services.AddScoped<IAzureBlobFileService, AzureBlobFileService>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IPasswordService, PasswordService>();

        }

        public static void AddEmailServices(this IServiceCollection services)
        {
            services.AddScoped<IBusisnessMailService, BusisnessMailService>();
        }

        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(configuration.GetSection("ApplicationOptions:PaginationOptions"));

            services.Configure<AzureBlobServiceOptions>(configuration.GetSection("ApplicationOptions:AzureBlobServiceOptions"));

            services.Configure<EmailServiceOptions>(configuration.GetSection("ApplicationOptions:EmailServiceOptions"));

            services.Configure<AuthenticationOptions>(configuration.GetSection("ApplicationOptions:AuthenticationOptions"));

            services.Configure<PasswordOptions>(configuration.GetSection("ApplicationOptions:PasswordOptions"));
        }

        public static void AddAzureClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration.GetConnectionString("DevelopmentAzureBlobStorage"));
            });
        }

        public static void AddJWTAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["ApplicationOptions:AuthenticationOptions:Authentication:Issuer"],
                    ValidAudience = configuration["ApplicationOptions:AuthenticationOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationOptions:AuthenticationOptions:Key"]))
                };
            });
        }
    }
}
