using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone) : IRequest<CustomerDto?>;
