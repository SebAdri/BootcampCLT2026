namespace CleanArchitecture.Simple.Application.Customers;

public record CustomerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime CreatedAt);
