using Domain.AccountMicroDomain.Models;
using Domain.AccountMicroDomain.Interfaces;
using MediatR;

namespace AccountMicro.Accounting.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IUnityOfWork _accountUnitOfWork;
        public CreateAccountCommandHandler(IUnityOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        Task<Unit> IRequestHandler<CreateAccountCommand, Unit>.Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            _accountUnitOfWork.Accounts.Add(new Account() { Name = command.name, Funds = command.funds });
            _accountUnitOfWork.Complete();
            return Task.FromResult(Unit.Value);
        }
    }
}
