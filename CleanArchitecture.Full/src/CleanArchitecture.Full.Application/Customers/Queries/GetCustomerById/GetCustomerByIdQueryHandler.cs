using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(ICustomerRepository repository) : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken);
        return customer?.ToDto();
    }
}
