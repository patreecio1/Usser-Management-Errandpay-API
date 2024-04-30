
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using ErrandPayApp.Infrastructure.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Core.Models;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Data;
using ErrandPayApp.Core.Interfaces.Managers;
using ErrandPayApp.Core.Managers;
using ErrandPayApp.Data.Repositories;
namespace ErrandPayApp.Infrastructure.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AppServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddHttpClient<IHttpService>("http", c => { })
                .ConfigurePrimaryHttpMessageHandler(h =>
                {

                    var handler = new HttpClientHandler();
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
                    return handler;
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //config
            //services.AddSingleton(configuration.GetSection("SuperAppUser").Get<AppUserDto>());
            services.AddSingleton(configuration.GetSection(nameof(AppUrl)).Get<AppUrl>());
            services.AddSingleton(configuration.GetSection(nameof(MailConfiguration)).Get<MailConfiguration>());
            services.AddScoped<IDapperContext, DapperContext>();
            services.AddSingleton(configuration.GetSection(nameof(UtilityConfig)).Get<UtilityConfig>());

            //services
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IHttpContextService, HttpContextService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IPermissionService, PermissionService>();
            //Manager
              services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IUserRoleManager, UserRoleManager>();

            //Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IConfirmationTokenRepository, ConfirmationTokenRepository>();
            services.AddScoped<IEmailSenderJob, EmailSenderJob>();
        }
        public static void SchedulerServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}

