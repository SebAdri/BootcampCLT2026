using CleanArchitecture.Simple.Domain;
using CleanArchitecture.Simple.Infrastructure.Persistence;
using CleanArchitecture.Simple.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Simple.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("CustomersDb"));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        return services;
    }
}
