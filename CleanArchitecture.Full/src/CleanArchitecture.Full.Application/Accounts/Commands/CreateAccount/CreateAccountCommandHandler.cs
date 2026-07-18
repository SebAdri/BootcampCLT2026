using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler(IAccountRepository repository) : IRequestHandler<CreateAccountCommand, AccountDto>
{
    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            AccountNumber = request.AccountNumber,
            HolderName = request.HolderName,
            Balance = request.Balance,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(account, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return account.ToDto();
    }
}
