using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(ICustomerRepository repository) : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(customer, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return customer.ToDto();
    }
}
