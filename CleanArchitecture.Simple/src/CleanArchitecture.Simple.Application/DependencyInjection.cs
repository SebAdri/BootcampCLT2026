using CleanArchitecture.Simple.Application.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Simple.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }
}
