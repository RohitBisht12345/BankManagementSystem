using BMS.Models.Entities;
using BMS.Services.Abstraction;
using BMS.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestProcessor, RequestProcessor>();
            services.AddScoped<IValidators<Accounts>, AccountValidators>();
            services.AddScoped<IValidators<Loans>, LoanValidators>();

            return services;
        }
    }
}
