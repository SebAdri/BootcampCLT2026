using CleanArchitecture.Full.Domain;

namespace CleanArchitecture.Full.Infrastructure.Persistence;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Customers.Any())
        {
            return;
        }

        context.Customers.AddRange(
            new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Ada",
                LastName = "Lovelace",
                Email = "ada.lovelace@example.com",
                Phone = "+595981000001",
                CreatedAt = DateTime.UtcNow
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Alan",
                LastName = "Turing",
                Email = "alan.turing@example.com",
                Phone = "+595981000002",
                CreatedAt = DateTime.UtcNow
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Grace",
                LastName = "Hopper",
                Email = "grace.hopper@example.com",
                Phone = "+595981000003",
                CreatedAt = DateTime.UtcNow
            });

        context.SaveChanges();
    }
}
