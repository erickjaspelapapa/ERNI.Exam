using ERNI.Data.Model;
using MediatR;

namespace ERNI.Reporsitory.Application.Transaction.Commands
{
    public class AccountTransCommand: IRequest<string>
    {
        public AccountModel Account { get; set; }
        public decimal Amount { get; set; }
    }
    public class AccountTransferCommand : IRequest<string>
    {
        public string AccountTranferFromId { get; set; }
        public string AccountTranferToId { get; set; }
        public decimal Amount { get; set; }
    }
}
