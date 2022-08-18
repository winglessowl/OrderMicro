
using Domain.AccountMicroDomain.Interfaces;
using Domain.AccountMicroDomain.Models;
using MediatR;

namespace AccountMicro.Accounting.Querys
{
    public class AccountsQueryHandler : IRequestHandler<AccountsQuery, List<Account>>
    {
        private readonly IUnityOfWork _accountUnitOfWork;
        public AccountsQueryHandler(IUnityOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }


        Task<List<Account>> IRequestHandler<AccountsQuery, List<Account>>.Handle(AccountsQuery request, CancellationToken cancellationToken)
        {
            return _accountUnitOfWork.Accounts.GetAll();
        }
    }
}
