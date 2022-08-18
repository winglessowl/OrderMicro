
namespace Domain.AccountMicroDomain.Interfaces
{
    public class AccountUnitOfWork : IUnityOfWork
    {
        public IAccountRepository Accounts { get; private set; }
        public AccountUnitOfWork(IAccountRepository Accounts) 
        {
            this.Accounts = Accounts;
        }
        public int Complete()
        {
            return this.Accounts.Complete();
        }

        public void Dispose()
        {
            this.Accounts.Dispose();
        }
    }
}
