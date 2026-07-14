using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerRepository repository) : IRequestHandler<UpdateCustomerCommand, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return null;
        }

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Email = request.Email;
        customer.Phone = request.Phone;

        repository.Update(customer);
        await repository.SaveChangesAsync(cancellationToken);

        return customer.ToDto();
    }
}
