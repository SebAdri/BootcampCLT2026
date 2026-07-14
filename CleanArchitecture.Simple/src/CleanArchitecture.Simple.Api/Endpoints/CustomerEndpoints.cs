using CleanArchitecture.Simple.Application.Customers;

namespace CleanArchitecture.Simple.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/minimal/customers").WithTags("Customers (Minimal API)");

        group.MapGet("", async (ICustomerService customerService, CancellationToken cancellationToken) =>
            Results.Ok(await customerService.GetAllAsync(cancellationToken)))
            .Produces<IEnumerable<CustomerDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:guid}", async (Guid id, ICustomerService customerService, CancellationToken cancellationToken) =>
        {
            var customer = await customerService.GetByIdAsync(id, cancellationToken);
            return customer is null ? Results.NotFound() : Results.Ok(customer);
        })
            .Produces<CustomerDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", async (CreateCustomerRequest request, ICustomerService customerService, CancellationToken cancellationToken) =>
        {
            var customer = await customerService.CreateAsync(request, cancellationToken);
            return Results.Created($"api/minimal/customers/{customer.Id}", customer);
        })
            .Produces<CustomerDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:guid}", async (Guid id, UpdateCustomerRequest request, ICustomerService customerService, CancellationToken cancellationToken) =>
        {
            var customer = await customerService.UpdateAsync(id, request, cancellationToken);
            return customer is null ? Results.NotFound() : Results.Ok(customer);
        })
            .Produces<CustomerDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:guid}", async (Guid id, ICustomerService customerService, CancellationToken cancellationToken) =>
        {
            var deleted = await customerService.DeleteAsync(id, cancellationToken);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
