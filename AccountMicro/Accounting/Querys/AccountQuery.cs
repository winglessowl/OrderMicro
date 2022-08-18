using Domain.AccountMicroDomain.Models;
using MediatR;

namespace AccountMicro.Accounting.Querys
{
    public record AccountQuery (int id) :IRequest<Account>
    {

    }
}
