using BMS.Infrastructure.Abstraction;
using BMS.Infrastructure.Implementation;
using BMS.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BmsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BMSConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
