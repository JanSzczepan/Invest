using Invest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invest.Infrastructure;

public static class Configure
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var authConnectionString =
            configuration.GetConnectionString("AuthenticationConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'AuthenticationConnection' not found."
            );
        var businessConnectionString =
            configuration.GetConnectionString("BusinessConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'BusinessConnection' not found."
            );
        services.AddDbContext<AuthenticationDbContext>(
            options => options.UseSqlServer(authConnectionString)
        );
        services.AddDbContext<BusinessDbContext>(
            options => options.UseSqlServer(businessConnectionString)
        );
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}
