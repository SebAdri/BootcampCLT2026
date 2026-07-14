using MediatR;

namespace CleanArchitecture.Full.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
