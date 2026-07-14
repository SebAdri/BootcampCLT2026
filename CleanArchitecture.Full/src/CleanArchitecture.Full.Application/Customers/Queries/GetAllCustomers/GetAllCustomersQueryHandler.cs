using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler(ICustomerRepository repository) : IRequestHandler<GetAllCustomersQuery, IReadOnlyList<CustomerDto>>
{
    public async Task<IReadOnlyList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync(cancellationToken);
        return customers.Select(c => c.ToDto()).ToList();
    }
}
