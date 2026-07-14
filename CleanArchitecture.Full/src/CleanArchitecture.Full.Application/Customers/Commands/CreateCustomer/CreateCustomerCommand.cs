using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone) : IRequest<CustomerDto>;
