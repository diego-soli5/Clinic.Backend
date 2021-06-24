﻿using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Options;
using Clinic.Core.Repositories.Interfaces;
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
        }

        public static void AddEmailServices(this IServiceCollection services)
        {
            services.AddScoped<IBusisnessMailService, BusisnessMailService>();
        }

        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(configuration.GetSection("ApplicationOptions:PaginationOptions"));

            services.Configure<AzureBlobServiceOptions>(configuration.GetSection("ApplicationOptions:AzureBlobServiceOptions"));

            services.Configure<EmailServiceOptions>(configuration.GetSection("ApplicationOptions:EmailSServiceOptions"));

        }

        public static void AddAzureClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration.GetConnectionString("DevelopmentAzureBlobStorage"));
            });
        }
    }
}
