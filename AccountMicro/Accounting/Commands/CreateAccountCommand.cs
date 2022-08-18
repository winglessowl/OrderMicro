using Domain.AccountMicroDomain.Models;
using MediatR;

namespace AccountMicro.Accounting.Commands
{
    public record CreateAccountCommand(string name,int funds) : IRequest
    {
    }
}
