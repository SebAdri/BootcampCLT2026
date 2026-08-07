using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler(IAccountRepository repository) : IRequestHandler<UpdateAccountCommand, AccountDto?>
{
    public async Task<AccountDto?> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (account is null)
        {
            return null;
        }

        account.AccountNumber = request.AccountNumber;
        account.HolderName = request.HolderName;
        account.Balance = request.Balance;
        account.Status = request.Status;

        repository.Update(account);
        await repository.SaveChangesAsync(cancellationToken);

        return account.ToDto();
    }
}
