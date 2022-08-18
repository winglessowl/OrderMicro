
using Domain.AccountMicroDomain.Models;
using MediatR;

namespace AccountMicro.Accounting.Querys
{
    public record AccountsQuery() : IRequest<List<Account>>
    {

    }
}

