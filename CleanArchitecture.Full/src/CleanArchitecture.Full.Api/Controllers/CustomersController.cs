using CleanArchitecture.Full.Application.Customers;
using CleanArchitecture.Full.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Full.Application.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Full.Application.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Full.Application.Customers.Queries.GetAllCustomers;
using CleanArchitecture.Full.Application.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Full.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var customers = await sender.Send(new GetAllCustomersQuery(), cancellationToken);
        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await sender.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Create(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Update(Guid id, UpdateCustomerRequestBody body, CancellationToken cancellationToken)
    {
        var customer = await sender.Send(new UpdateCustomerCommand(id, body.FirstName, body.LastName, body.Email, body.Phone), cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteCustomerCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateCustomerRequestBody(string FirstName, string LastName, string Email, string Phone);
