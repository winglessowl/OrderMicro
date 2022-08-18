
namespace Domain.AccountMicroDomain.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        int Complete();
    }
}
