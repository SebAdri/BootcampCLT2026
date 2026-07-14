using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<IReadOnlyList<CustomerDto>>;
