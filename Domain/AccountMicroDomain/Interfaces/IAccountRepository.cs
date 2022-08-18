

using Domain.AccountMicroDomain.Models;

namespace Domain.AccountMicroDomain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        ValueTask<Account?> GetAccountById(int id);
        int Complete();
        void Dispose();
    }
}
