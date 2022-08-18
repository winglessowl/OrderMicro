using Domain.AccountMicroDomain.Interfaces;
using Domain.AccountMicroDomain.Models;

namespace Repositories.AccountMicroRepositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(AccountContext context) : base(context)
        {

        }
        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public ValueTask<Account?> GetAccountById(int id)
        {
            return AccountContext.Accounts.FindAsync(id);
        }


        public AccountContext AccountContext { get { return context as AccountContext; } }
    }
}
