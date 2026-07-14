using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(ICustomerRepository repository) : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return false;
        }

        repository.Remove(customer);
        await repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
