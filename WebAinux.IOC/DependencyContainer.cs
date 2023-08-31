using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.Services;

namespace WebAdinux.IOC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUser, UserService>();
            services.AddTransient<IEmailMessage, EmailMessageService>();
        }
    }
}