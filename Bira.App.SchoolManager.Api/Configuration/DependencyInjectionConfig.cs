using Bira.App.SchoolManager.Domain.Extensions;
using Bira.App.SchoolManager.Domain.Interfaces.Repositories;
using Bira.App.SchoolManager.Domain.Interfaces;
using Bira.App.SchoolManager.Infra.Repositories.BaseContext;
using Bira.App.SchoolManager.Infra.Repositories;
using Bira.App.SchoolManager.Service.Interfaces;
using Bira.App.SchoolManager.Service.Notifications;
using Bira.App.SchoolManager.Service.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bira.App.SchoolManager.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApiDbContext>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}