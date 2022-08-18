
using Domain.AccountMicroDomain.Interfaces;
using Domain.AccountMicroDomain.Models;
using MediatR;

namespace AccountMicro.Accounting.Querys
{
    public class AccountQueryHandler : IRequestHandler<AccountQuery, Account>
    {
        private readonly IUnityOfWork _accountUnitOfWork;
        public AccountQueryHandler(IUnityOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        public Task<Account> Handle(AccountQuery query, CancellationToken cancellationToken)
        {

            return _accountUnitOfWork.Accounts.GetAccountById(query.id).AsTask();
        }
    }
}
