using CleanArchitecture.Full.Application.Customers;
using CleanArchitecture.Full.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Full.Application.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Full.Application.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Full.Application.Customers.Queries.GetAllCustomers;
using CleanArchitecture.Full.Application.Customers.Queries.GetCustomerById;
using MediatR;

namespace CleanArchitecture.Full.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/minimal/customers").WithTags("Customers (Minimal API)");

        group.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
            Results.Ok(await sender.Send(new GetAllCustomersQuery(), cancellationToken)))
            .Produces<IReadOnlyList<CustomerDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var customer = await sender.Send(new GetCustomerByIdQuery(id), cancellationToken);
            return customer is null ? Results.NotFound() : Results.Ok(customer);
        })
            .Produces<CustomerDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", async (CreateCustomerCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var customer = await sender.Send(command, cancellationToken);
            return Results.Created($"api/minimal/customers/{customer.Id}", customer);
        })
            .Produces<CustomerDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:guid}", async (Guid id, UpdateCustomerBody body, ISender sender, CancellationToken cancellationToken) =>
        {
            var customer = await sender.Send(new UpdateCustomerCommand(id, body.FirstName, body.LastName, body.Email, body.Phone), cancellationToken);
            return customer is null ? Results.NotFound() : Results.Ok(customer);
        })
            .Produces<CustomerDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var deleted = await sender.Send(new DeleteCustomerCommand(id), cancellationToken);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}

public record UpdateCustomerBody(string FirstName, string LastName, string Email, string Phone);
