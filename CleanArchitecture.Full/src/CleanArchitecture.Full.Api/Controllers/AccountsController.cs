using CleanArchitecture.Full.Application.Accounts;
using CleanArchitecture.Full.Application.Accounts.Commands.CreateAccount;
using CleanArchitecture.Full.Application.Accounts.Commands.DeleteAccount;
using CleanArchitecture.Full.Application.Accounts.Commands.UpdateAccount;
using CleanArchitecture.Full.Application.Accounts.Queries.GetAllAccounts;
using CleanArchitecture.Full.Application.Accounts.Queries.GetAccountById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Full.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]

    public async Task<ActionResult<IReadOnlyList<AccountDto>>> GetAll(CancellationToken cancellationToken)
    {
        var accounts = await sender.Send(new GetAllAccountsQuery(), cancellationToken);
        return Ok(accounts);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AccountDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var account = await sender.Send(new GetAccountByIdQuery(id), cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AccountDto>> Create(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AccountDto>> Update(Guid id, UpdateAccountRequestBody body, CancellationToken cancellationToken)
    {
        var account = await sender.Send(new UpdateAccountCommand(id, body.AccountNumber, body.HolderName, body.Balance, body.Status), cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteAccountCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateAccountRequestBody(string AccountNumber, string HolderName, decimal Balance, string Status);
